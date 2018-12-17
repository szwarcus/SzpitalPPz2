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
            builder.Property(x => x.UserName).HasMaxLength(50);

            builder.HasOne(x => x.Doctor)
                   .WithOne(x => x.User)
                   .HasForeignKey<Doctor>(x => x.UserId)
                   .IsRequired(false);

            builder.HasOne(x => x.Patient)
                   .WithOne(x => x.User)
                   .HasForeignKey<Patient>(x => x.UserId)
                   .IsRequired(false);

            builder.HasOne(x => x.Nurse)
                   .WithOne(x => x.User)
                   .HasForeignKey<Nurse>(x => x.UserId)
                   .IsRequired(false);

            builder.HasOne(x => x.Admin)
                   .WithOne(x => x.User)
                   .HasForeignKey<Admin>(x => x.UserId)
                   .IsRequired(false);
        }
    }
}