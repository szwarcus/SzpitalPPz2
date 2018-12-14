using Hospital.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital.Model.Configurations
{
    public class PrescriptionMedicamentConfiguration : IEntityTypeConfiguration<PrescriptionMedicament>
    {
        public void Configure(EntityTypeBuilder<PrescriptionMedicament> builder)
        {
            builder.HasKey(x => new { x.PrescriptionId, x.MedicamentId });

            builder.HasOne(x => x.Prescription)
                   .WithMany(x => x.PrescriptionMedicaments)
                   .HasForeignKey(x => x.PrescriptionId);

            builder.HasOne(x => x.Medicament)
                 .WithMany(x => x.PrescriptionMedicaments)
                 .HasForeignKey(x => x.MedicamentId);
        }
    }
}
