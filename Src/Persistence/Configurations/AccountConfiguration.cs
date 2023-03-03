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
    public class AccountConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.HasMany(a => a.SavingsIn)
                .WithOne(s => s.ToAccount)
                .HasForeignKey(s => s.ToAccountId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(a => a.SavingsOut)
                .WithOne(s => s.FromAccount)
                .HasForeignKey(s => s.FromAccountId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(a => a.Transactions)
                .WithOne(t => t.Account)
                .HasForeignKey(t => t.AccountId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(a => a.FutureTransactions)
                .WithOne(ft => ft.Account)
                .HasForeignKey(ft => ft.AccountId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(a => a.FutureSavingsIn)
                .WithOne(fs => fs.ToAccount)
                .HasForeignKey(fs => fs.ToAccountId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(a => a.FutureSavingsOut)
                .WithOne(fs => fs.FromAccount)
                .HasForeignKey(fs => fs.FromAccountId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}