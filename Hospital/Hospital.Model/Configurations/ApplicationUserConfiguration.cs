using System;
using Hospital.Core.Enums;
using Hospital.Model.Entities;
using Hospital.Model.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hospital.Model.Configurations
{
    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Email).HasMaxLength(50);
            builder.Property(x => x.City).HasMaxLength(50);
            builder.Property(x => x.FirstName).HasMaxLength(30);
            builder.Property(x => x.LastName).HasMaxLength(30);
            builder.Property(x => x.NIP).HasMaxLength(30);
            builder.Property(x => x.PESEL).HasMaxLength(20);
            builder.Property(x => x.PhoneNumber).HasMaxLength(20);
            builder.Property(x => x.Province).HasMaxLength(30);
            builder.Property(x => x.PostalCode).HasMaxLength(10);
            builder.Property(x => x.Street).HasMaxLength(30);
            builder.Property(x => x.SystemRoleName).HasMaxLength(15);
            builder.Property(x => x.UserName).HasMaxLength(50);

            builder.Property(x => x.SystemRoleName).HasMaxLength(50)
                                                   .IsRequired();

            builder.HasOne(x => x.Doctor)
                   .WithOne(x => x.User)
                   .HasForeignKey<Doctor>(x => x.UserId)
                   .IsRequired(false);

            builder.HasOne(x => x.Patient)
                   .WithOne(x => x.User)
                   .HasForeignKey<Patient>(x => x.UserId)
                   .IsRequired(false);

            builder.HasData(
                new ApplicationUser(Role.Doctor)
                {
                    Id = "9f2cfdcb-9f4d-4d65-a81c-43deacf27741",
                    UserName = "doctor@test.com",
                    Email = "doctor@test.com",
                    PasswordHash = "AQAAAAEAACcQAAAAECKLtts8yfs643jZ79ss7Oj7shA9VVpWxwCwDN361Rn93O6aHWvMzquScKdHxFdLQQ==",
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
                },
                new ApplicationUser(Role.Patient)
                {
                    Id = "9f2cfdcb-9f4d-4d65-a81c-43deacf27742",
                    UserName = "patient@test.com",
                    PasswordHash = "AQAAAAEAACcQAAAAECKLtts8yfs643jZ79ss7Oj7shA9VVpWxwCwDN361Rn93O6aHWvMzquScKdHxFdLQQ==", //Admin1.
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
                });
        }
    }
}