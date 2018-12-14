using Hospital.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital.Model.Configurations
{
    public class VaccineAppliedConfigurations : IEntityTypeConfiguration<VaccineApplied>
    {
        public void Configure(EntityTypeBuilder<VaccineApplied> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Patient)
                   .WithMany(x => x.VaccineApplieds)
                   .HasForeignKey(x => x.PatientId)
                   .IsRequired();

            builder.HasOne(x => x.Nurse)
                   .WithMany(x => x.VaccineApplieds)
                   .HasForeignKey(x => x.NurseId)
                   .IsRequired();

            builder.HasOne(x => x.Vaccine)
                   .WithMany(x => x.VaccineApplieds)
                   .HasForeignKey(x => x.VaccineId)
                   .IsRequired();
        }
    }
}
