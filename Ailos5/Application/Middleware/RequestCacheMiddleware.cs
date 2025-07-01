using AilosInfra.Bases.Dtos;
using AilosInfra.Interfaces.DataBase.RedisDb.UnitOfWorkFactory;
using AilosInfra.Settings.DataBases.RedisDb.Settings;
using Domain.Entities.Redis;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Text;
using System.Text.Json;

namespace Application.Middleware
{
    public class RequestCacheMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ConnectionSettings _ConnectionSettings;

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

            var factory = context.RequestServices.GetRequiredService<IUnitOfWorkFactory<Idempotence>>();
            var unit = await factory.Create(_ConnectionSettings);
            var cached = await unit.GetAsync(new CommandSettings<Idempotence>
            {
                Entity = new Idempotence(),
                Predicate = x => x.Guid == key,
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

            var factory = context.RequestServices.GetRequiredService<IUnitOfWorkFactory<Idempotence>>();
            var unit = await factory.Create(_ConnectionSettings);

            await unit.InsertAsync(new CommandSettings<Idempotence>
            {
                Entity = new Idempotence
                {
                    Guid = key,
                    Value = responseBody,
                    Created = DateTime.UtcNow
                },
                ExpireItem = TimeSpan.FromDays(1)
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
    }
}
