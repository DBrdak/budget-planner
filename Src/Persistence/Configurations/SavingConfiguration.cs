using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

public class SavingConfiguration : IEntityTypeConfiguration<Saving>
{
    public void Configure(EntityTypeBuilder<Saving> builder)
    {
        builder.Property(s => s.Date).HasColumnType("Date");

        builder.HasOne(s => s.Goal)
            .WithMany(g => g.Savings)
            .HasForeignKey(s => s.GoalId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}