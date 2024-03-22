
using StudentApp.API.Common;
using System.Text.Json;

namespace StudentApp.API.Middleware
{
    public class GlobalExceptionMiddleware : IMiddleware
    {
        private readonly ILogger<GlobalExceptionMiddleware> _logger;

        public GlobalExceptionMiddleware(ILogger<GlobalExceptionMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next.Invoke(context);
                if (context.Response.StatusCode == StatusCodes.Status401Unauthorized)
                {
                    await PrepareUnAuthorizeResponseAsync(context);
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                await PrepareErrorResponseAsync(context, e);
            }
        }

        public async Task PrepareErrorResponseAsync(HttpContext context, Exception e)
        {
            context.Response.ContentType = "application/json";
            var response = JsonSerializer.Serialize(APIResponse<string>.Error(e.Message, StatusCodes.Status500InternalServerError), StaticDeclaration.camelCase);
            await context.Response.WriteAsync(response);
        }

        public async Task PrepareUnAuthorizeResponseAsync(HttpContext context)
        {
            context.Response.ContentType = "application/json";
            var response = JsonSerializer.Serialize(APIResponse<string>.Error("Request is not authorized", StatusCodes.Status401Unauthorized), StaticDeclaration.camelCase);
            await context.Response.WriteAsync(response);
        }
    }
}
