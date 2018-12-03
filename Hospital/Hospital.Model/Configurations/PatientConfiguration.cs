using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Hospital.Model.Entities;

namespace Hospital.Model.Configurations
{
    public class PatientConfiguration : IEntityTypeConfiguration<Patient>
    {
        public void Configure(EntityTypeBuilder<Patient> builder)
        {
            builder.HasKey(x => x.Id); 

            builder.HasData(
                new Patient
                {
                    Id = 1,
                    UserId = "9f2cfdcb-9f4d-4d65-a81c-43deacf27742"
                });
        }
    }
}