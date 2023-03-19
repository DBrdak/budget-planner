using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

public class BudgetConfiguration : IEntityTypeConfiguration<Budget>
{
    public void Configure(EntityTypeBuilder<Budget> builder)
    {
        builder.HasOne(b => b.User)
            .WithOne()
            .HasForeignKey<Budget>(b => b.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(b => b.FutureTransactions)
            .WithOne(ft => ft.Budget)
            .HasForeignKey(ft => ft.BudgetId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(b => b.Transactions)
            .WithOne(t => t.Budget)
            .HasForeignKey(t => t.BudgetId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(b => b.FutureSavings)
            .WithOne(fs => fs.Budget)
            .HasForeignKey(fs => fs.BudgetId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(b => b.Savings)
            .WithOne(s => s.Budget)
            .HasForeignKey(s => s.BudgetId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(b => b.Accounts)
            .WithOne(a => a.Budget)
            .HasForeignKey(a => a.BudgetId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(b => b.Goals)
            .WithOne(g => g.Budget)
            .HasForeignKey(g => g.BudgetId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(b => b.TransactionCategories)
            .WithOne(tc => tc.Budget)
            .HasForeignKey(tc => tc.BudgetId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}