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
            builder.Property(x => x.FirstName).HasMaxLength(50);
            builder.Property(x => x.LastName).HasMaxLength(50);

            builder.HasOne(x => x.Doctor)
                   .WithOne(x => x.User)
                   .HasForeignKey<Doctor>(x => x.UserId)
                   .IsRequired(false);

            builder.HasOne(x => x.Patient)
                   .WithOne(x => x.User)
                   .HasForeignKey<Patient>(x => x.UserId)
                   .IsRequired(false);
        }
    }
}