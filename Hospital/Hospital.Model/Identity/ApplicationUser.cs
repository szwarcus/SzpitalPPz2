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
        public DateTime LastLoginTime { get; set; }
        public bool IsActive { get; set; }

        public DateTime DateOfBirth { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string NIP { get; set; }
        public string PESEL { get; set; }
        public string Street { get; set; }
        public string Province { get; set; }

        public virtual Doctor Doctor { get; set; }
        public virtual Patient Patient { get; set; }
        public virtual Admin Admin { get; set; }
        public virtual Nurse Nurse { get; set; }

        /* to do
         * avatar - jpg
        */

        public ApplicationUser()
        {
            Created = DateTime.UtcNow;
            LastLoginTime = DateTime.UtcNow;
        }
    }
}