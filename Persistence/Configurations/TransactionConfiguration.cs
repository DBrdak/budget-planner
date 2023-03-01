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
    public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.Property(t => t.Date).HasColumnType("Date");

            builder.HasOne(t => t.FutureTransaction)
                .WithMany(ft => ft.CompletedTransactions)
                .HasForeignKey(t => t.FutureTransactionId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}