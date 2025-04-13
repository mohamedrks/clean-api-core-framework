namespace clean_api_core_framework.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IHostEnvironment _env;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context); // Continue down pipeline
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occurred while processing request {Path}", context.Request.Path);
                _logger.LogError(ex, "Unhandled exception");

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;

                object response = _env.IsDevelopment()
                    ? new { message = ex.Message, stackTrace = ex.StackTrace }
                    : new { message = "An internal server error occurred." };

                await context.Response.WriteAsJsonAsync(response);
            }
        }
    }
}
