using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AuthApp.API.Filters
{
    public class RoleAuthorizationFilter : IAsyncAuthorizationFilter
    {
        private readonly string _claimType;
        private readonly string _value;
        public RoleAuthorizationFilter(string claimType, string value)
        {
            _claimType = claimType;
            _value = value;
        }
        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            if (context.HttpContext.User == null || !context.HttpContext.User.Identity.IsAuthenticated)
            {
                throw new UnauthorizedAccessException("Invalid or expired token found");
            }

            if (!context.HttpContext.User.Claims.Any(x => x.Type.Trim().ToLower() == _claimType.Trim().ToLower() && x.Value.Trim().ToLower() == _value.Trim().ToLower()))
            {
                throw new UnauthorizedAccessException("Invalid access requested");

            }
        }
    }
}
