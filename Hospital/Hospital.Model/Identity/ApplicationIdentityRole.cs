using Microsoft.AspNetCore.Identity;

namespace Hospital.Model.Identity
{
    public class ApplicationIdentityRole : IdentityRole<string>
    {
        public ApplicationIdentityRole() : base() { }
        public ApplicationIdentityRole(string roleName) : base(roleName) { }
    }
}