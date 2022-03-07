using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace martin_resume_api.Auth
{
    public class AnyRoleRequirementHandler : AuthorizationHandler<AnyRoleRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AnyRoleRequirement requirement)
        {
            if (!context.User.HasClaim(c => c.Type == ClaimTypes.Role))
            {
                return Task.CompletedTask;
            }

            var claims = context.User.FindAll(x => x.Type == ClaimTypes.Role);
            if (claims == null || claims.Count() == 0)
            {
                return Task.CompletedTask;
            }

            foreach (var role in requirement.Roles)
            {
                if (claims.Any(x => x.Value == Enum.GetName(typeof(AuthRole), role)))
                {
                    context.Succeed(requirement);
                    break;
                }
            }

            return Task.CompletedTask;
        }
    }
}

