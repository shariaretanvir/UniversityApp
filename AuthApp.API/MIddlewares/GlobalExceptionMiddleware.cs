
using AuthApp.API.Common;
using System.Text.Json;

namespace AuthApp.API.MIddlewares
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
                if(context.Response.StatusCode == StatusCodes.Status401Unauthorized)
                {
                    _logger.LogError("UnAuthorize request");
                    await PrepareAuthorizeResponse(context);
                }
			}
			catch (Exception e)
			{
                _logger.LogError(e.Message);
                await PrepareErrorResponse(context, e);
			}
        }

        public async Task PrepareErrorResponse(HttpContext context, Exception e)
        {
            context.Response.ContentType = "application/json";
            var response = JsonSerializer.Serialize(APIResponse<string>.Error(e.Message, "Internal Server Errror", 500), StaticDeclaration.camelCase);
            await context.Response.WriteAsync(response);
        }

        public async Task PrepareAuthorizeResponse(HttpContext context)
        {
            context.Response.ContentType = "application/json";
            var response = JsonSerializer.Serialize(APIResponse<string>.Error("", "UnAuthorize access requested"), StaticDeclaration.camelCase);
            await context.Response.WriteAsync(response);
        }
    }
}
