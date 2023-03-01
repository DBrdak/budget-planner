using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Configurations
{
    public class SavingConfiguration : IEntityTypeConfiguration<Saving>
    {
        public void Configure(EntityTypeBuilder<Saving> builder)
        {
            builder.Property(s => s.Date).HasColumnType("Date");

            builder.HasOne(s => s.Goal)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(s => s.FutureSaving)
                .WithMany(fs => fs.CompletedSavings)
                .HasForeignKey(s => s.FutureSavingId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}