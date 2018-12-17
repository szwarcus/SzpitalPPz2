using System;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Hospital.Model.Identity;
using Hospital.Core.Enums;
using Hospital.Model.Entities;

namespace Hospital.Repository.Concrete
{
    public static class DbInitializer
    {
        #region Users
        private static ApplicationUser doctor = new ApplicationUser
        {
            UserName = "doctor@test.com",
            Email = "doctor@test.com",
            FirstName = "Marian",
            LastName = "Nowak",
            City = "Toruń",
            DateOfBirth = DateTime.UtcNow,
            Gender = GenderType.Male,
            PESEL = "11111111111",
            PhoneNumber = "123456789",
            Province = "Kujawsko Pomorskie",
            PostalCode = "87-100",
            Street = "Szeroka 10"
        };

        private static ApplicationUser patient = new ApplicationUser
        {
            UserName = "patient@test.com",
            Email = "patient@test.com",
            FirstName = "Piotr",
            LastName = "Kiepski",
            City = "Toruń",
            DateOfBirth = DateTime.UtcNow,
            Gender = GenderType.Male,
            PESEL = "11111111112",
            PhoneNumber = "123456780",
            Province = "Kujawsko Pomorskie",
            PostalCode = "87-100",
            Street = "Szeroka 10"
        };

        private static ApplicationUser nurse = new ApplicationUser
        {
            UserName = "nurse@test.com",
            Email = "nurse@test.com",
            FirstName = "Katarzyna",
            LastName = "Boczek",
            City = "Toruń",
            DateOfBirth = DateTime.UtcNow,
            Gender = GenderType.Female,
            PESEL = "11111181112",
            PhoneNumber = "123256780",
            Province = "Kujawsko Pomorskie",
            PostalCode = "87-100",
            Street = "Długa 11"
        };

        private static ApplicationUser admin = new ApplicationUser
        {
            UserName = "admin@test.com",
            Email = "admin@test.com",
            FirstName = "Mariusz",
            LastName = "Tester",
            City = "Toruń",
            DateOfBirth = DateTime.UtcNow,
            Gender = GenderType.Female,
            PESEL = "11111181113",
            PhoneNumber = "123335678",
            Province = "Kujawsko Pomorskie",
            PostalCode = "87-100",
            Street = "Długa 11"
        };
        #endregion

        #region Public Methods
        public static void SeedData (UserManager<ApplicationUser> userManager,
                                     RoleManager<ApplicationIdentityRole> roleManager,
                                     IApplicationBuilder appBuilder)
        {
            var serviceProvider = appBuilder.ApplicationServices.CreateScope();
            var context = serviceProvider.ServiceProvider.GetService(typeof(ApplicationDbContext)) as ApplicationDbContext;

            SeedOtherDataForUsers(context).Wait();

            SeedRoles(roleManager).Wait();

            SeedUsers(userManager, context).Wait();

            SeedOtherData(context).Wait();
        }
        #endregion

        #region Private Methods
        private static async Task SeedUsers(UserManager<ApplicationUser> userManager, 
                                            ApplicationDbContext context)
        {
            await SeedPatient(userManager, context);
            await SeedDoctor(userManager, context);
            await SeedNurse(userManager, context);
            await SeedAdmin(userManager, context);
        }

        private static async Task SeedRoles(RoleManager<ApplicationIdentityRole> roleManager)
        {
            foreach (var roleName in Enum.GetNames(typeof(SystemRoleType)))
            {
                if (!roleManager.RoleExistsAsync(roleName).Result)
                {
                    await roleManager.CreateAsync(new ApplicationIdentityRole(roleName));
                }
            }
        }

        private static async Task SeedOtherDataForUsers(ApplicationDbContext context)
        {
            var harmonogram = await context.Harmonograms.Where(x => x.Id == 1)
                                                        .FirstOrDefaultAsync();

            if (harmonogram != null)
            {
                return;
            }

            await context.AddAsync(new Harmonogram {  });

            await context.AddAsync(new NurseSpecialization
            {
                Name = "Onkologiczna"
            });

            await context.AddAsync(new Specialization
            {
                Name = "Dentysta"
            });

            await context.AddAsync(new Vaccine()
            {
                Name = "VaxigripTetra",
                Dosage = "(ml/kg)",
                Description = "Grypa"
            });

            await context.SaveChangesAsync();
        }

        private static async Task SeedOtherData(ApplicationDbContext context)
        {
            // TO DO in the feature if needed
        }

        private static async Task SeedPatient(UserManager<ApplicationUser> userManager,
                                              ApplicationDbContext context)
        {
            var createdUserPatient = await AssignUserToRole(userManager, patient, SystemRoleType.Patient);

            if (createdUserPatient == null)
            {
                return;
            }

            createdUserPatient.EmailConfirmed = true;
            await userManager.UpdateAsync(createdUserPatient);

            context.Add(new Patient
            {
                UserId = createdUserPatient.Id
            });

            await context.SaveChangesAsync();
        }

        private static async Task SeedDoctor(UserManager<ApplicationUser> userManager,
                                             ApplicationDbContext context)
        {
            var createdUserDoctor = await AssignUserToRole(userManager, doctor, SystemRoleType.Doctor);

            if (createdUserDoctor == null)
            {
                return;
            }

            createdUserDoctor.EmailConfirmed = true;
            await userManager.UpdateAsync(createdUserDoctor);

            context.Add(new Doctor
            {
                UserId = createdUserDoctor.Id,
                SpecializationId = 1,
                HarmonogramId = 1
            });

            await context.SaveChangesAsync();
        }

        private static async Task SeedNurse(UserManager<ApplicationUser> userManager,
                                            ApplicationDbContext context)
        {
            var createdUserNurse = await AssignUserToRole(userManager, nurse, SystemRoleType.Nurse);

            if (createdUserNurse == null)
            {
                return;
            }

            createdUserNurse.EmailConfirmed = true;
            await userManager.UpdateAsync(createdUserNurse);

            context.Add(new Nurse
            {
                UserId = createdUserNurse.Id,
                NurseSpecializationId = 1
            });

            await context.SaveChangesAsync();
        }

        private static async Task SeedAdmin(UserManager<ApplicationUser> userManager,
                                            ApplicationDbContext context)
        {
            var createdUserAdmin = await AssignUserToRole(userManager, admin, SystemRoleType.Admin);

            if (createdUserAdmin == null)
            {
                return;
            }

            createdUserAdmin.EmailConfirmed = true;
            await userManager.UpdateAsync(createdUserAdmin);

            context.Add(new Admin
            {
                UserId = createdUserAdmin.Id
            });

            await context.SaveChangesAsync();
        }


        private static async Task<ApplicationUser> AssignUserToRole(UserManager<ApplicationUser> userManager,
                                                                    ApplicationUser user,
                                                                    SystemRoleType role)
        {
            var userInDb = await userManager.FindByNameAsync(user.UserName);

            if (userInDb != null)
            {
                return null;
            }

            var result = await userManager.CreateAsync(user, "Admin1.");

            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, role.ToString());

                return userManager.FindByNameAsync(user.UserName).Result;
            }

            return null;
        }
        #endregion
    }
}