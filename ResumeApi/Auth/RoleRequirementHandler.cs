﻿using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace ResumeApi.Auth
{
    public class RoleRequirementHandler : AuthorizationHandler<RoleRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, RoleRequirement requirement)
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

            if (claims.Any(x => x.Value == Enum.GetName(typeof(AuthRole), requirement.Role)))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}

