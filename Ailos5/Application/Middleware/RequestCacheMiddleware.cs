using AilosInfra.Bases.Dtos;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Linq;
using System.Data.SqlClient;
using System.Runtime.InteropServices;
using System.Text.Json;
using DapperDomain = Domain.Entities.Sql;
using DapperIUnitFactory = AilosInfra.Interfaces.DataBase.Dapper.UnitOfWorkFactory;
using DapperIUnit = AilosInfra.Interfaces.DataBase.Dapper.UnitOfWork;
using DapperSettings = AilosInfra.Settings.DataBases.Dapper.Settings;
using RedisDomain = Domain.Entities.Redis;
using RedisIunitFactory = AilosInfra.Interfaces.DataBase.RedisDb.UnitOfWorkFactory;
using RedisIunit = AilosInfra.Interfaces.DataBase.RedisDb.UnitOfWork;
using RedisSettings = AilosInfra.Settings.DataBases.RedisDb.Settings;

namespace Application.Middleware
{
    public class RequestCacheMiddleware
    {
        private readonly RequestDelegate _next;
        private DapperSettings.ConnectionSettings _DapperConnectionSettings;
        private RedisSettings.ConnectionSettings _RedisConnectionSettings;

        public RequestCacheMiddleware(
            RequestDelegate next,
            DapperSettings.ConnectionSettings dapperConnectionSettings,
            RedisSettings.ConnectionSettings redisConnectionSettings)
        {
            _next = next;
            _DapperConnectionSettings = dapperConnectionSettings;
            _RedisConnectionSettings = redisConnectionSettings;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            context.Request.EnableBuffering();

            var key = await GetKeyRequestAsync(context);

            if (await TryReturnCachedResponseRedisAsync(context, key))
                return;

            await CaptureResponseAndProceedRedisAsync(context, key);
        }

        #region Dapper
        private async Task<bool> TryReturnCachedResponseDapperAsync(HttpContext context, Guid key)
        {
            if (key == Guid.Empty) return false;

            var unit = await CreateNewFactoryDapper(context);

            var parameters = new DynamicParameters();
            parameters.Add("@Guid", key);

            var cached = await unit.GetAsync(new DapperSettings.CommandSettings<DapperDomain.Idempotence>
            {
                Entity = new DapperDomain.Idempotence(),
                CommandType = DapperSettings.TypeCommand.Query,
                Query = @"SELECT TOP 1 [Value] FROM [dbo].[Idempotence] WHERE [Guid] = @Guid AND [Deleted] = 0",
                Parameters = parameters
            });

            if (cached != null)
            {
                unit = await CreateNewFactoryDapper(context);
                _ = await unit.UpdateAsync(new DapperSettings.CommandSettings<DapperDomain.Idempotence>()
                {
                    Entity = new DapperDomain.Idempotence(),
                    CommandType = DapperSettings.TypeCommand.Query,
                    Query = @"UPDATE [dbo].[Idempotence] SET [Deleted] = 1 WHERE [Guid] = @Guid",
                    Parameters = parameters
                });

                context.Response.StatusCode = 200;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(cached.Value);
                return true;
            }

            return false;
        }
        private async Task CaptureResponseAndProceedDapperAsync(HttpContext context, Guid key)
        {
            var originalBody = context.Response.Body;
            using var memStream = new MemoryStream();
            context.Response.Body = memStream;

            context.Request.Body.Position = 0;
            await _next(context);

            memStream.Seek(0, SeekOrigin.Begin);
            string responseBody = await new StreamReader(memStream).ReadToEndAsync();

            memStream.Seek(0, SeekOrigin.Begin);
            await memStream.CopyToAsync(originalBody);
            context.Response.Body = originalBody;

            await SaveResponseAsyncDapper(context, key, responseBody);
        }
        private async Task SaveResponseAsyncDapper(HttpContext context, Guid key, string responseBody)
        {
            if (key == Guid.Empty || string.IsNullOrWhiteSpace(responseBody))
                return;

            var obj = JsonSerializer.Deserialize<BaseResponse>(responseBody, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            if (!obj.Success)
                return;

            var unit = await CreateNewFactoryDapper(context);

            string query = @"INSERT INTO[dbo].[Idempotence] ([Value], [Guid], [Created], [Updated], [Deleted])
                    VALUES(@Value, @Guid, @Created, @Updated, @Deleted);";

            var parameter = new DynamicParameters();
            parameter.Add("@Value", responseBody);
            parameter.Add("@Guid", key);
            parameter.Add("@Created", DateTime.UtcNow);
            parameter.Add("@Updated", DateTime.UtcNow);
            parameter.Add("@Deleted", false);

            await unit.InsertAsync(new DapperSettings.CommandSettings<DapperDomain.Idempotence>
            {
                Entity = new DapperDomain.Idempotence
                {
                    Guid = key,
                    Value = responseBody,
                    Created = DateTime.UtcNow
                },
                CommandType = DapperSettings.TypeCommand.Query,
                Parameters = parameter,
                Query = query
            });
        }
        private async Task<DapperIUnit.IUnitOfWork<DapperDomain.Idempotence>> CreateNewFactoryDapper(HttpContext context)
        {
            DapperSettings.ConnectionSettings connection;
            if (_DapperConnectionSettings.Connection.State == System.Data.ConnectionState.Closed)
            {
                connection = new DapperSettings.ConnectionSettings
                {
                    Connection = new SqlConnection(_DapperConnectionSettings.ConnectionString),
                    ConnectionString = _DapperConnectionSettings.ConnectionString,
                    EnableTransaction = _DapperConnectionSettings.EnableTransaction
                };
            }
            else
                connection = _DapperConnectionSettings;

            var factory = context.RequestServices.GetRequiredService<DapperIUnitFactory.IUnitOfWorkFactory<DapperDomain.Idempotence>>();
            return await factory.Create(connection);
        }
        #endregion

        #region Redis
        private async Task<bool> TryReturnCachedResponseRedisAsync(HttpContext context, Guid key)
        {
            if (key == Guid.Empty) return false;

            var unit = await CreateNewFactoryRedis(context);
            var cached = await unit.GetAsync(new RedisSettings.CommandSettings<RedisDomain.Idempotence>
            {
                Entity = new RedisDomain.Idempotence() { Guid = key },
                DeleteAfterReader = true
            });


            if (cached != null)
            {
                context.Response.StatusCode = 200;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(cached.Value);
                return true;
            }

            return false;
        }
        private async Task CaptureResponseAndProceedRedisAsync(HttpContext context, Guid key)
        {
            var originalBody = context.Response.Body;
            using var memStream = new MemoryStream();
            context.Response.Body = memStream;

            context.Request.Body.Position = 0;
            await _next(context);

            memStream.Seek(0, SeekOrigin.Begin);
            string responseBody = await new StreamReader(memStream).ReadToEndAsync();

            memStream.Seek(0, SeekOrigin.Begin);
            await memStream.CopyToAsync(originalBody);
            context.Response.Body = originalBody;

            await SaveResponseAsyncRedis(context, key, responseBody);
        }
        private async Task SaveResponseAsyncRedis(HttpContext context, Guid key, string responseBody)
        {
            if (key == Guid.Empty || string.IsNullOrWhiteSpace(responseBody))
                return;

            var obj = JsonSerializer.Deserialize<BaseResponse>(responseBody, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            if (!obj.Success)
                return;

            var unit = await CreateNewFactoryRedis(context);

            _ = unit.InsertAsync(new RedisSettings.CommandSettings<RedisDomain.Idempotence>()
            {
                Entity = new RedisDomain.Idempotence
                {
                    Guid = key,
                    Value = responseBody,
                    Created = DateTime.UtcNow
                },
            });
        }
        private async Task<RedisIunit.IUnitOfWork<RedisDomain.Idempotence>> CreateNewFactoryRedis(HttpContext context)
        {
            var factory = context.RequestServices.GetRequiredService<RedisIunitFactory.IUnitOfWorkFactory<RedisDomain.Idempotence>>();
            var response = await factory.Create(_RedisConnectionSettings);
            return response;
        }
        #endregion

        private async Task<Guid> GetKeyRequestAsync(HttpContext context)
        {
            context.Request.Body.Position = 0;

            string bodyAsText = await new StreamReader(context.Request.Body).ReadToEndAsync();
            context.Request.Body.Position = 0;

            if (!string.IsNullOrWhiteSpace(bodyAsText))
            {
                var obj = JsonSerializer.Deserialize<BaseRequest>(bodyAsText, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                if (obj?.KeyRequest != Guid.Empty)
                    return obj.KeyRequest;
            }

            if (context.Request.Query.TryGetValue("KeyRequest", out var keyVal) &&
                Guid.TryParse(keyVal, out var key) && key != Guid.Empty)
            {
                return key;
            }

            return Guid.Empty;
        }


    }
}
