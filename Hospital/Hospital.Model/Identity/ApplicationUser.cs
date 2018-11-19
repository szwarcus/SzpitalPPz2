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
        public string SystemRoleName { get; private set; }

        public virtual Doctor Doctor { get; set; }
        public virtual Patient Patient { get; set; }

        public ApplicationUser()
        {
            Created = DateTime.UtcNow;
        }

        public ApplicationUser(string roleName)
        {
            Created = DateTime.UtcNow;
            SystemRoleName = roleName;
        }
    }
}