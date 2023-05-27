using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using App.Common.Enums;

namespace App.Helper
{
    public class AuthorizeApiUser : AuthorizeAttribute, IAuthorizationFilter
    {
        public Roles[] Roles { get; set; }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var userRoles = context.HttpContext.User.Claims.Where(c => c.Type == ClaimTypes.Role);
            var hasRole = userRoles.Any(u => Roles == null || Roles.Any(r => r.ToString() == u.Value));
            if (!hasRole)
            {
                context.Result = new UnauthorizedResult();
            }
        }
    }
}
