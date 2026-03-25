using ERP_System.Application.Interfaces;

namespace ERP_System.API.Presentation
{
    public class IdempotencyMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<IdempotencyMiddleware> _logger;

        private static readonly HashSet<string> _idempotentMethods = new()
         {
            HttpMethods.Post,
            HttpMethods.Put,
            HttpMethods.Delete
        };
        public IdempotencyMiddleware(RequestDelegate next, ILogger<IdempotencyMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, ICacheService cache)
        {
            if (!_idempotentMethods.Contains(context.Request.Method))
            {
                await _next(context);
                return;
            }

            if(!context.Request.Headers.TryGetValue("Idempotency-Key",out var idempotencyKey)
                                                    || string.IsNullOrWhiteSpace(idempotencyKey))
            {
                await _next(context);
                return;
            }

            var cacheKey = $"idempotency:{idempotencyKey}";

            var cached = await cache.GetAsync<IdempotencyResponse>(cacheKey, context.RequestAborted);
            if( cached is not null)
            {
                _logger.LogInformation("Idempotency HIT -> Key : {Key} | Status : {Status}",
                    idempotencyKey, cached.StatusCode);

                context.Response.StatusCode = cached.StatusCode;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(cached.Body);
                return;
            }

            var originalBody = context.Response.Body;

            using var memStream = new MemoryStream();
            context.Response.Body = memStream;

            await _next(context);

            memStream.Seek(0, SeekOrigin.Begin);    
            var responseBody = await new StreamReader(memStream).ReadToEndAsync();

            if(context.Response.StatusCode >= 200 && context.Response.StatusCode < 300)
            {
                var idempotencyResponse = new IdempotencyResponse
                {
                    StatusCode = context.Response.StatusCode,
                    Body = responseBody
                };

                await cache.SetAsync(
                    cacheKey,
                    idempotencyResponse,
                    TimeSpan.FromHours(24),
                    context.RequestAborted);

                _logger.LogInformation("Idempotency Stored -> Key : {Key}", idempotencyKey);
            }
            memStream.Seek(0, SeekOrigin.Begin);
            await memStream.CopyToAsync(originalBody);
            context.Response.Body = originalBody;
        }
    
    }
    public class IdempotencyResponse
    {
        public int StatusCode { get; set; }
        public string Body { get; set; } = string.Empty;
    }
}
