using Hospital.Core.Enums;
using Microsoft.AspNetCore.Identity;

namespace Hospital.Model.Identity
{
    public class ApplicationIdentityRole : IdentityRole
    {
        public ApplicationIdentityRole() : base() { }
        public ApplicationIdentityRole(string roleName) : base(roleName) { }
    }
}