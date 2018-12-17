using System.Threading.Tasks;
using Hospital.Core.Enums;
using Hospital.Model.Identity;
using Hospital.Repository.Abstract;
using Microsoft.AspNetCore.Identity;

namespace Hospital.Repository.Concrete
{
    public class UserRepository : IUserRepository
    {
        private RoleManager<ApplicationIdentityRole> _roleManager;
        private UserManager<ApplicationUser> _userManager;

        public UserRepository(RoleManager<ApplicationIdentityRole> roleManager,
                              UserManager<ApplicationUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task<bool> ConfirmEmailAsync(ApplicationUser user, string token)
        {  
            if (user == null)
                return false;
            var result = await _userManager.ConfirmEmailAsync(user,token);

            return result.Succeeded ? true : false; 
        }

        public async Task<ApplicationUser> CreateAsync(ApplicationUser entity, SystemRoleType role, string password)
        {
            ApplicationUser result = null;

            var user = await _userManager.FindByEmailAsync(entity.Email);

            if (user != null)
            {
                return result;
            }

            var createAsyncResult = await _userManager.CreateAsync(entity, password);

            if (createAsyncResult.Succeeded)
            {
                var roleName = role.ToString();

                if (!await _roleManager.RoleExistsAsync(roleName))
                {
                    await _roleManager.CreateAsync(new ApplicationIdentityRole(roleName));
                }

                await _userManager.AddToRoleAsync(entity, roleName);

                result = await _userManager.FindByEmailAsync(entity.Email);
            }

            return result;
        }

        public async Task<ApplicationUser> FindAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            return user;
        }

    }
}