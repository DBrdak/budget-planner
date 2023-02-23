using Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
    public class DataContext : IdentityDbContext<User>
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Account>(b =>
            {
                b.HasMany(a => a.SavingsIn)
                    .WithOne(s => s.ToAccount)
                    .HasForeignKey(s => s.ToAccountId)
                    .OnDelete(DeleteBehavior.Cascade);

                b.HasMany(a => a.SavingsOut)
                    .WithOne(s => s.FromAccount)
                    .HasForeignKey(s => s.FromAccountId)
                    .OnDelete(DeleteBehavior.Cascade);

                b.HasMany(a => a.Transactions)
                    .WithOne(t => t.Account)
                    .HasForeignKey(t => t.AccountId)
                    .OnDelete(DeleteBehavior.Cascade);

                b.HasMany(a => a.FutureTransactions)
                    .WithOne(ft => ft.Account)
                    .HasForeignKey(ft => ft.AccountId)
                    .OnDelete(DeleteBehavior.Cascade);

                b.HasMany(a => a.FutureSavingsIn)
                    .WithOne(fs => fs.ToAccount)
                    .HasForeignKey(fs => fs.ToAccountId)
                    .OnDelete(DeleteBehavior.Cascade);

                b.HasMany(a => a.FutureSavingsOut)
                    .WithOne(fs => fs.FromAccount)
                    .HasForeignKey(fs => fs.FromAccountId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            builder.Entity<Saving>(b =>
            {
                b.HasOne(s => s.Goal)
                    .WithMany()
                    .OnDelete(DeleteBehavior.Restrict);

                b.HasOne(s => s.FutureSaving)
                    .WithMany(fs => fs.CompletedSavings)
                    .HasForeignKey(s => s.FutureSavingId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            builder.Entity<Transaction>(b =>
            {
                b.HasOne(t => t.FutureTransaction)
                    .WithMany(ft => ft.CompletedTransactions)
                    .HasForeignKey(t => t.FutureTransactionId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            builder.Entity<Budget>(b =>
            {
                b.HasOne(b => b.User)
                    .WithOne()
                    .HasForeignKey<Budget>(b => b.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

                b.HasMany(b => b.FutureTransactions)
                    .WithOne(ft => ft.Budget)
                    .HasForeignKey(ft => ft.BudgetId)
                    .OnDelete(DeleteBehavior.Cascade);

                b.HasMany(b => b.Transactions)
                    .WithOne(t => t.Budget)
                    .HasForeignKey(t => t.BudgetId)
                    .OnDelete(DeleteBehavior.Cascade);

                b.HasMany(b => b.FutureSavings)
                    .WithOne(fs => fs.Budget)
                    .HasForeignKey(fs => fs.BudgetId)
                    .OnDelete(DeleteBehavior.Cascade);

                b.HasMany(b => b.Savings)
                    .WithOne(s => s.Budget)
                    .HasForeignKey(s => s.BudgetId)
                    .OnDelete(DeleteBehavior.Cascade);

                b.HasMany(b => b.Accounts)
                    .WithOne(a => a.Budget)
                    .HasForeignKey(a => a.BudgetId)
                    .OnDelete(DeleteBehavior.Cascade);

                b.HasMany(b => b.Goals)
                    .WithOne(g => g.Budget)
                    .HasForeignKey(g => g.BudgetId)
                    .OnDelete(DeleteBehavior.Cascade);

                b.HasMany(b => b.TransactionCategories)
                    .WithOne(tc => tc.Budget)
                    .HasForeignKey(tc => tc.BudgetId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Budget> Budgets { get; set; }
        public DbSet<FutureSaving> FutureSavings { get; set; }
        public DbSet<FutureTransaction> FutureTransactions { get; set; }
        public DbSet<Goal> Goals { get; set; }
        public DbSet<Saving> Savings { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<TransactionCategory> TransactionCategories { get; set; }
    }
}