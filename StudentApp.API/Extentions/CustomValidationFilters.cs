using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using StudentApp.API.Common;
using System.Text.Json;

namespace StudentApp.API.Extentions
{
    public class CustomValidationFilters : IAsyncActionFilter
    {
        private readonly ILogger<CustomValidationFilters> _logger;

        public CustomValidationFilters(ILogger<CustomValidationFilters> logger)
        {
            _logger = logger;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.ModelState.IsValid)
            {
                var errors = context.ModelState.Values.Where(x => x.Errors.Any())
                    .SelectMany(x => x.Errors)
                    .Select(x => x.ErrorMessage)
                    .ToList();

                var response = JsonSerializer.Serialize(APIResponse<List<string>>.Error(errors, "Validation Failed"), StaticDeclaration.camelCase);
                _logger.LogError(response);

                context.Result = new BadRequestObjectResult(response);
                return;
            }
            await next.Invoke();
        }
    }
}
