namespace WebServer.Middleware
{
    public class RequestLimiterMiddleware
    {
        private readonly RequestDelegate _next;
        private static int _currentRequests = 0;
        private readonly int _parallelLimit;

        public RequestLimiterMiddleware(RequestDelegate next, int parallelLimit)
        {
            _next = next;
            _parallelLimit = parallelLimit;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (Interlocked.Increment(ref _currentRequests) > _parallelLimit)
            {
                Interlocked.Decrement(ref _currentRequests);
                context.Response.StatusCode = StatusCodes.Status503ServiceUnavailable;
                await context.Response.WriteAsync("Сервис недоступен: слишком много запросов.");
                return;
            }

            try
            {
                await _next(context);
            }
            finally
            {
                Interlocked.Decrement(ref _currentRequests);
            }
        }
    }
}
