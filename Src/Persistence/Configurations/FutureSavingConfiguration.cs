using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

public class FutureSavingConfiguration : IEntityTypeConfiguration<FutureSaving>
{
    public void Configure(EntityTypeBuilder<FutureSaving> builder)
    {
        builder.Property(fs => fs.Date).HasColumnType("Date");

        builder.HasMany(ft => ft.CompletedSavings)
            .WithOne(t => t.FutureSaving)
            .HasForeignKey(t => t.FutureSavingId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}