using Hospital.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital.Model.Configurations
{
    public class NurseSpecializationConfiguration : IEntityTypeConfiguration<NurseSpecialization>
    {
        public void Configure(EntityTypeBuilder<NurseSpecialization> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasData(
                new NurseSpecialization
                {
                    Id = 1,
                    Name = "Onkologiczna"
                });
        }
    }
}
