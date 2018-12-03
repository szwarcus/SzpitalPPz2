using Hospital.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hospital.Model.Configurations
{
    public class SpecializationConfiguration : IEntityTypeConfiguration<Specialization>
    {
        public void Configure(EntityTypeBuilder<Specialization> builder)
        {
            builder.HasKey(x => x.Id);

            // zmienic stringa na jakiegos enuma
            builder.HasData(
                new Specialization
                {
                    Id = 1,
                    Name = "Dentysta"
                });
        }
    }
}
