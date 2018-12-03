using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Hospital.Model.Entities;

namespace Hospital.Model.Configurations
{
    public class DoctorConfiguration : IEntityTypeConfiguration<Doctor>
    {
        public void Configure(EntityTypeBuilder<Doctor> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Specialization)
                   .WithMany(x => x.Doctors)
                   .HasForeignKey(x => x.SpecializationId)
                   .IsRequired();

            builder.HasOne(x => x.Harmonogram)
                   .WithOne(x => x.Doctor)
                   .HasForeignKey<Doctor>(x => x.HarmonogramId);

            // zmienic dentyste na jakiegos enuma czy cos
            builder.HasData(
                new Doctor
                {
                    Id = 1,
                    UserId = "9f2cfdcb-9f4d-4d65-a81c-43deacf27741",
                    SpecializationId = 1,
                    HarmonogramId = 1
                });
        }
    }
}