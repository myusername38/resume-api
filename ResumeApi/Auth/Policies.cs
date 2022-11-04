using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace ResumeApi.Auth
{
    public class Policies
    {
        public static AuthorizationPolicy Owner()
        {
            var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser();
            policy.Requirements.Add(new RoleRequirement(AuthRole.Owner));
            return policy.Build();
        }

        public static AuthorizationPolicy Follower()
        {
            var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser();
            policy.Requirements.Add(new AnyRoleRequirement(new List<AuthRole> { AuthRole.Owner, AuthRole.Follower }));
            return policy.Build();
        }

        public static AuthorizationPolicy AnyPolicy()
        {
            var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser();
            return policy.Build();
        }
    }
}

