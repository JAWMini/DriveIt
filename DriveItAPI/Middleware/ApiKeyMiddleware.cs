namespace DriveItAPI.Middleware
{
    public class ApiKeyMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly List<String> _validAPIKeys;

        public ApiKeyMiddleware(RequestDelegate next, List<String> validAPIKeys)
        {
            _next = next;
            _validAPIKeys = validAPIKeys;
        }

        public async Task Invoke(HttpContext context)
        {
            if (!context.Request.Headers.TryGetValue("X-Api-Key", out var extractedApiKey))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("API Key was not provided. Please provide an API Key.");
                return;
            }

          

            if (!_validAPIKeys.Contains(extractedApiKey))
            {
                context.Response.StatusCode = 403;
                await context.Response.WriteAsync("Invalid API key");
                return;
            }

            await _next(context);
        }
    }
}
