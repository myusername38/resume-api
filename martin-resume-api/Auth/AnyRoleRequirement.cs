using System;
using Microsoft.AspNetCore.Authorization;

namespace martin_resume_api.Auth
{
    public class AnyRoleRequirement : IAuthorizationRequirement
    {
        public AnyRoleRequirement(List<AuthRole> roles)
        {
            Roles = roles;
        }

        public List<AuthRole> Roles { get; set; }
    }
}

