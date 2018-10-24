using System;
using Hospital.Core.Enums;
using Hospital.Model.Entities;
using Microsoft.AspNetCore.Identity;

namespace Hospital.Model.Identity
{
    public class ApplicationUser : IdentityUser<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public GenderType Gender { get; set; }

        public DateTime Created { get; set; }

        public virtual Doctor Doctor { get; set; }

        public ApplicationUser()
        {
            Created = DateTime.UtcNow;
        }
    }
}
