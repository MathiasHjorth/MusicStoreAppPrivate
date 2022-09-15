namespace MusicStoreAppTwo.Middleware
{
    public class RequestMiddleware
    {

        private readonly ILogger<RequestMiddleware> _logger;
        private readonly RequestDelegate _next;

        public RequestMiddleware(ILogger<RequestMiddleware> logger, RequestDelegate next)
        {
            _logger = logger;
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            _logger.LogInformation("Hejsa det er mig");
            await _next.Invoke(context);
        }
    }
}