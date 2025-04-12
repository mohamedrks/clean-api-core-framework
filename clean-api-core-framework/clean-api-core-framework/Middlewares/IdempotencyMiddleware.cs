using Application.Interfaces;

namespace APICoreFramework.Middlewares
{
    public class IdempotencyMiddleware
    {
        private readonly RequestDelegate _next;

        public IdempotencyMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IIdempotencyRepository store)
        {
            if (context.Request.Method != HttpMethods.Post || !context.Request.Headers.TryGetValue("Idempotency-Key", out var key))
            {
                await _next(context);
                return;
            }

            var idempotencyKey = key.ToString();
            var existing = await store.GetResponseAsync(idempotencyKey);

            if (existing != null)
            {
                context.Response.StatusCode = 200;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(existing);
                return;
            }

            var originalBody = context.Response.Body;
            using var memoryStream = new MemoryStream();
            context.Response.Body = memoryStream;

            await _next(context);

            memoryStream.Seek(0, SeekOrigin.Begin);
            var responseBody = await new StreamReader(memoryStream).ReadToEndAsync();

            var cancellationToken = context.RequestAborted;
            await store.SaveResponseAsync(idempotencyKey, responseBody, cancellationToken);

            memoryStream.Seek(0, SeekOrigin.Begin);
            await memoryStream.CopyToAsync(originalBody);
            context.Response.Body = originalBody;
        }
    }

}