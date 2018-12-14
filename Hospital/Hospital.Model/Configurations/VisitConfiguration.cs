using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Hospital.Model.Entities;

namespace Hospital.Model.Configurations
{
    public class VisitConfiguration : IEntityTypeConfiguration<Visit>
    {
        public void Configure(EntityTypeBuilder<Visit> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Patient)
                   .WithMany(x => x.Visits)
                   .HasForeignKey(x => x.PatientId)
                   .IsRequired();

            builder.HasOne(x => x.Doctor)
                   .WithMany(x => x.Visits)
                   .HasForeignKey(x => x.DoctorId)
                   .IsRequired();

           builder.HasOne(x => x.Prescription)
                 .WithOne(x => x.Visit)
                 .HasForeignKey<Prescription>(x => x.VisitId);
        }
    }
}