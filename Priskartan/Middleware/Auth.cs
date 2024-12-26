namespace Priskartan.Middleware;

public class Auth
{
    private readonly RequestDelegate _requestDelegate;
    private readonly IConfiguration _configuration;

    public Auth(RequestDelegate requestDelegate, IConfiguration configuration)
    {
        _requestDelegate = requestDelegate;
        _configuration = configuration;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        if (!httpContext.Request.Headers.TryGetValue("Authorization", out var authToken) ||
            string.IsNullOrEmpty(authToken) ||
            authToken != _configuration["PriskartanAuthToken"])
        {
            httpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await httpContext.Response.WriteAsync("Unauthorized");
            return;
        }

        await _requestDelegate(httpContext);
    }
}
