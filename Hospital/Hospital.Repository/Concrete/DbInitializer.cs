using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Hospital.Model.Identity;
using Hospital.Repository.Abstract;

namespace Hospital.Repository.Concrete
{
    public class DbInitializer : IDbInitializer
    {
        private readonly ApplicationDbContext _context;
        private readonly RoleManager<ApplicationIdentityRole> _roleManager;

        public DbInitializer(ApplicationDbContext context, RoleManager<ApplicationIdentityRole> roleManager)
        {
            _context = context;
            _roleManager = roleManager;
        }

        public async Task Initialize()
        {
            // create database schema if none exists
            _context.Database.EnsureCreated();

            await CreateAdminRole();
            await CreatePatientRole();
        }

        private async Task CreateAdminRole()
        {
            // if there is already an Administrator role, abort
            var adminRoleExists = await _roleManager.RoleExistsAsync(Role.Admin);

            if (!adminRoleExists)
            {
                // Create the Admin Role
                var adminRole = new ApplicationIdentityRole(Role.Admin);
                var result = await _roleManager.CreateAsync(adminRole);

                if (result.Succeeded)
                {
                    //var foreverTrialClaim = new Claim("Trial", DateTime.Now.AddYears(1).ToString());
                    //await _roleManager.AddClaimAsync(adminRole, foreverTrialClaim);
                }
            }
        }

        private async Task CreatePatientRole()
        {
            // if there is already an Administrator role, abort
            var patientRoleExists = await _roleManager.RoleExistsAsync(Role.Patient);

            if (!patientRoleExists)
            {
                // Create the Admin Role
                var patientRole = new ApplicationIdentityRole(Role.Patient);
                var result = await _roleManager.CreateAsync(patientRole);

                if (result.Succeeded)
                {
                    //var foreverTrialClaim = new Claim("Trial", DateTime.Now.AddYears(1).ToString());
                    //await _roleManager.AddClaimAsync(adminRole, foreverTrialClaim);
                }
            }
        }
    }
}
