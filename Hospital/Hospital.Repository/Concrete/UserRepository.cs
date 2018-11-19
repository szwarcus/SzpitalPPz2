using System.Threading.Tasks;
using Hospital.Model.Identity;
using Hospital.Repository.Abstract;
using Microsoft.AspNetCore.Identity;

namespace Hospital.Repository.Concrete
{
    public class UserRepository : IUserRepository
    {
        private SignInManager<ApplicationUser> _signInManager;
        private RoleManager<ApplicationIdentityRole> _roleManager;
        private UserManager<ApplicationUser> _userManager;

        public UserRepository(SignInManager<ApplicationUser> signInManager, 
                              RoleManager<ApplicationIdentityRole> roleManager,
                              UserManager<ApplicationUser> userManager)
        {
            _signInManager = signInManager;
            _roleManager = roleManager;
            _userManager = userManager;
        }


        public async Task<bool> CreateAsync(ApplicationUser entity, string password)
        {
            var result = false;

            var user = await _userManager.FindByEmailAsync(entity.Email);

            if (user != null)
            {
                return result;
            }

            var createAsyncResult = await _userManager.CreateAsync(entity, password);

            if (createAsyncResult.Succeeded && Role.Exists(entity.RoleName))
            {
                var roleExists = await _roleManager.RoleExistsAsync(entity.RoleName);

                if (!roleExists)
                {
                    await _roleManager.CreateAsync(new ApplicationIdentityRole(entity.RoleName));
                }

                await _userManager.AddToRoleAsync(entity, entity.RoleName);

                result = true;
            }

            return result;
        }
    }
}