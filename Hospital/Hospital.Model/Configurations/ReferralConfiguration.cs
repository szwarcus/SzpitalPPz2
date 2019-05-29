using Hospital.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital.Model.Configurations
{
    public class ReferralConfiguration : IEntityTypeConfiguration<Referral>
    {
        public void Configure(EntityTypeBuilder<Referral> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Specialization)
                   .WithMany(x => x.Referrals)
                   .HasForeignKey(x => x.SpecializationId)
                   .IsRequired();

            builder.HasOne<Visit>(x => x.Visit)
                   .WithOne(x => x.Referral)
                   .HasForeignKey<Referral>(x => x.VisitId)
                   .OnDelete(DeleteBehavior.Restrict)
                   .IsRequired();
            
        }
    }
}
