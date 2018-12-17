using Hospital.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital.Model.Configurations
{
    public class NurseConfigurations : IEntityTypeConfiguration<Nurse>
    {
        public void Configure(EntityTypeBuilder<Nurse> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.NurseSpecialization)
                   .WithMany(x => x.Nurses)
                   .HasForeignKey(x => x.NurseSpecializationId)
                   .IsRequired();
        }
    }
}
