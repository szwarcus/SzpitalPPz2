using System;
using Microsoft.AspNetCore.Authorization;
using Hospital.Core.Enums;

namespace Hospital.Infrastructure.Attributes
{
    public class RolesAttribute : AuthorizeAttribute
    {
        public RolesAttribute(params SystemRoleType[] roles)
        {
            Roles = String.Join(",", roles);
        }
    }
}