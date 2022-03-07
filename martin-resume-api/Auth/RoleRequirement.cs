using System;
using Microsoft.AspNetCore.Authorization;

namespace martin_resume_api.Auth
{
    public class RoleRequirement : IAuthorizationRequirement
    {
        public RoleRequirement(AuthRole role)
        {
            Role = role;
        }

        public AuthRole Role { get; set; }
    }
}

