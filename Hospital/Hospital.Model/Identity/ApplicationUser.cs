using System;
using Microsoft.AspNetCore.Identity;
using Hospital.Core.Enums;
using Hospital.Model.Entities;

namespace Hospital.Model.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public GenderType Gender { get; set; }

        public DateTime Created { get; set; }
        public string RoleName { get; private set; }

        public virtual Doctor Doctor { get; set; }
        public virtual Patient Patient { get; set; }

        public ApplicationUser(string roleName)
        {
            Created = DateTime.UtcNow;
            RoleName = roleName;
        }
    }
}