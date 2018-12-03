using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Hospital.Model.Entities;

namespace Hospital.Model.Configurations
{
    public class HarmonogramConfiguration : IEntityTypeConfiguration<Harmonogram>
    {
        public void Configure(EntityTypeBuilder<Harmonogram> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasData(
                new Harmonogram
                {
                    Id = 1
                });
        }
    }
}