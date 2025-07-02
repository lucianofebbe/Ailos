using AilosInfra.Bases.Dtos;
using AilosInfra.Interfaces.DataBase.Dapper.UnitOfWork;
using AilosInfra.Interfaces.DataBase.Dapper.UnitOfWorkFactory;
using AilosInfra.Settings.DataBases.Dapper.Settings;
using Dapper;
using Domain.Entities.Sql;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data.SqlClient;
using System.Text;
using System.Text.Json;

namespace Application.Middleware
{
    public class RequestCacheMiddleware
    {
        private readonly RequestDelegate _next;
        private ConnectionSettings _ConnectionSettings;

        public RequestCacheMiddleware(RequestDelegate next, ConnectionSettings connectionSettings)
        {
            _next = next;
            _ConnectionSettings = connectionSettings;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            context.Request.EnableBuffering();

            var key = await GetKeyRequestAsync(context);

            if (await TryReturnCachedResponseAsync(context, key))
                return;

            await CaptureResponseAndProceedAsync(context, key);
        }

        private async Task<bool> TryReturnCachedResponseAsync(HttpContext context, Guid key)
        {
            if (key == Guid.Empty) return false;

            var unit = await CreateNewFactory(context);

            var parameters = new DynamicParameters();
            parameters.Add("@Guid", key);

            var cached = await unit.GetAsync(new CommandSettings<Idempotence>
            {
                Entity = new Idempotence(),
                CommandType = TypeCommand.Query,
                Query = @"SELECT TOP 1 [Value] FROM [dbo].[Idempotence] WHERE [Guid] = @Guid AND [Deleted] = 0",
                Parameters = parameters
            });

            if (cached != null)
            {
                unit = await CreateNewFactory(context);
                _ = await unit.UpdateAsync(new CommandSettings<Idempotence>()
                {
                    Entity = new Idempotence(),
                    CommandType = TypeCommand.Query,
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

        private async Task CaptureResponseAndProceedAsync(HttpContext context, Guid key)
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

            await SaveResponseAsync(context, key, responseBody);
        }

        private async Task SaveResponseAsync(HttpContext context, Guid key, string responseBody)
        {
            if (key == Guid.Empty || string.IsNullOrWhiteSpace(responseBody))
                return;

            var obj = JsonSerializer.Deserialize<BaseResponse>(responseBody, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            if (!obj.Success)
                return;

            var unit = await CreateNewFactory(context);

            string query = @"INSERT INTO[dbo].[Idempotence] ([Value], [Guid], [Created], [Updated], [Deleted])
                    VALUES(@Value, @Guid, @Created, @Updated, @Deleted);";

            var parameter = new DynamicParameters();
            parameter.Add("@Value", responseBody);
            parameter.Add("@Guid", key);
            parameter.Add("@Created", DateTime.UtcNow);
            parameter.Add("@Updated", DateTime.UtcNow);
            parameter.Add("@Deleted", false);

            await unit.InsertAsync(new CommandSettings<Idempotence>
            {
                Entity = new Idempotence
                {
                    Guid = key,
                    Value = responseBody,
                    Created = DateTime.UtcNow
                },
                CommandType = TypeCommand.Query,
                Parameters = parameter,
                Query = query
            });
        }

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

        private async Task<IUnitOfWork<Idempotence>> CreateNewFactory(HttpContext context)
        {
            ConnectionSettings connection;
            if (_ConnectionSettings.Connection.State == System.Data.ConnectionState.Closed)
            {
                connection = new ConnectionSettings
                {
                    Connection = new SqlConnection(_ConnectionSettings.ConnectionString),
                    ConnectionString = _ConnectionSettings.ConnectionString,
                    EnableTransaction = _ConnectionSettings.EnableTransaction
                };
            }
            else
                connection = _ConnectionSettings;

            var factory = context.RequestServices.GetRequiredService<IUnitOfWorkFactory<Idempotence>>();
            return await factory.Create(connection);
        }
    }
}
