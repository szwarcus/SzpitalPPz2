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
            SeedMedicaments(context).Wait();
        }
        #endregion

        #region Private Methods
        private static async Task SeedUsers(UserManager<ApplicationUser> userManager, 
                                            ApplicationDbContext context)
        {
            await SeedPatient(userManager, context);
            await SeedDoctors(userManager, context);
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
            await SeedSpecializations(context);
            await SeedHarmonograms(context);

            var nurseSpecialization = await context.NurseSpecializations.Where(x => x.Id == 1)
                                                                        .FirstOrDefaultAsync();

            if (nurseSpecialization != null)
            {
                return;
            }

            await context.AddAsync(new NurseSpecialization
            {
                Name = "Onkologiczna"
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
            
        }

        private static async Task SeedMedicaments(ApplicationDbContext context)
        {
            await SeedMedicament(context, "Apap", "Ból głowy");
            await SeedMedicament(context, "No-spa", "Lek wywiera działanie rozkurczające na mięśnie gładkie przewodu pokarmowego, układu moczowo-płciowego, układu krążenia oraz dróg żółciowych");
            await SeedMedicament(context, "Amoksiklav", "Amoksiklav to preparat bakteriobójczy, stosowany w zakażeniach różnego typu: dolnych i górnych dróg oddechowych, układu moczowego, skóry, kości, stawów. Preparat zawiera antybiotyk beta-laktamowy. Amoksiklav jest dostępny na receptę.");
        }
        private static async Task SeedMedicament(ApplicationDbContext context, string name , string description)
        {
            await context.AddAsync(new Medicament() {
                Description = description,
                Name = name
            });
            await context.SaveChangesAsync();

        }

        private static async Task SeedSpecializations(ApplicationDbContext context)
        {
            if (await context.Specializations.CountAsync() > 2)
            {
                return;
            }

            await context.AddAsync(new Specialization
            {
                Name = "Dentysta"
            });

            await context.AddAsync(new Specialization
            {
                Name = "Okulista"
            });

            await context.AddAsync(new Specialization
            {
                Name = "Psychiatra"
            });

            await context.SaveChangesAsync();
        }

        private static async Task SeedHarmonograms(ApplicationDbContext context)
        {
            if (await context.Harmonograms.CountAsync() > 4)
            {
                return;
            }

            var start10am = new TimeSpan(10, 0, 0);
            var end4pm = new TimeSpan(16, 0, 0);

            for (int i=0; i < 5; i++)
            {
                await context.AddAsync(new Harmonogram
                {
                    MondayStart = start10am,
                    MondayEnd = end4pm,
                    TuesdayStart = start10am,
                    TuesdayEnd = end4pm,
                    WednesdayStart = start10am,
                    WednesdayEnd = end4pm,
                    ThursdayStart = start10am,
                    ThursdayEnd = end4pm,
                    FridayStart = start10am,
                    FridayEnd = end4pm,
                });
            }

            await context.SaveChangesAsync();
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

        private static async Task SeedDoctors(UserManager<ApplicationUser> userManager,
                                              ApplicationDbContext context)
        {
            var doctors = await userManager.GetUsersInRoleAsync(SystemRoleType.Nurse.ToString());

            if (doctors.Count > 4)
            {
                return;
            }

            await SeedDoctor(userManager, context, "Jan", "Nowak", 1, 1, "doctor@test.com");
            await SeedDoctor(userManager, context, "Janina", "Bąk", 2, 2, "doctor2@test.com");
            await SeedDoctor(userManager, context, "Piotr", "Kowalski", 3, 3, "doctor3@test.com");
            await SeedDoctor(userManager, context, "Mateusz", "Wiśniewski", 2, 4, "doctor4@test.com");
            await SeedDoctor(userManager, context, "Kamil", "Kowalczyk", 1, 5, "doctor5@test.com");
        }

        private static async Task SeedDoctor(UserManager<ApplicationUser> userManager,
                                             ApplicationDbContext context,
                                             string firstName,
                                             string lastName,
                                             int specializationId,
                                             int harmonogramId,
                                             string email)
        {
            ApplicationUser doctor = new ApplicationUser
            {
                UserName = email,
                Email = email,
                FirstName = firstName,
                LastName = lastName,
                City = "Toruń",
                DateOfBirth = DateTime.UtcNow,
                Gender = GenderType.Male,
                PESEL = "11111111111",
                PhoneNumber = "123456789",
                Province = "Kujawsko Pomorskie",
                PostalCode = "87-100",
                Street = "Szeroka 10"
            };

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
                SpecializationId = specializationId,
                HarmonogramId = harmonogramId
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