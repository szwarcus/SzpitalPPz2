using Hospital.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital.Model.Configurations
{
    public class VaccineConfigurations : IEntityTypeConfiguration<Vaccine>
    {
        public void Configure(EntityTypeBuilder<Vaccine> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasData(new Vaccine() {
                Id = 1,
                Name = "VaxigripTetra",
                Dosage = "(X ml/kg ",
                Description = "Grypa"
            });
        }
    }
}
