using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class FutureTransactionConfiguration : IEntityTypeConfiguration<FutureTransaction>

    {
        public void Configure(EntityTypeBuilder<FutureTransaction> builder)
        {
            builder.Property(ft => ft.Date).HasColumnType("Date");

            builder.HasMany(ft => ft.CompletedTransactions)
                .WithOne(t => t.FutureTransaction)
                .HasForeignKey(t => t.FutureTransactionId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}