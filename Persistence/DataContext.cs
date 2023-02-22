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

            builder.Entity<Account>()
                .HasMany(a => a.SavingsIn)
                .WithOne(s => s.ToAccount)
                .HasForeignKey(s => s.ToAccountId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Account>()
                .HasMany(a => a.SavingsOut)
                .WithOne(s => s.FromAccount)
                .HasForeignKey(s => s.FromAccountId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Account>()
                .HasMany(a => a.Transactions)
                .WithOne(t => t.Account)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Saving>()
                .HasOne(s => s.Goal)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Budget>()
                .HasOne(b => b.User)
                .WithOne()
                .HasPrincipalKey<User>(u => u.Id)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Budget>()
                .HasMany(b => b.FutureTransactions)
                .WithOne(ft => ft.Budget)
                .HasForeignKey(ft => ft.BudgetId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Budget>()
                .HasMany(b => b.Transactions)
                .WithOne(t => t.Budget)
                .HasForeignKey(t => t.BudgetId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Budget>()
                .HasMany(b => b.FutureSavings)
                .WithOne(fs => fs.Budget)
                .HasForeignKey(fs => fs.BudgetId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Budget>()
                .HasMany(b => b.Savings)
                .WithOne(s => s.Budget)
                .HasForeignKey(s => s.BudgetId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Budget>()
                .HasMany(b => b.Accounts)
                .WithOne(a => a.Budget)
                .HasForeignKey(a => a.BudgetId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Budget>()
                .HasMany(b => b.Goals)
                .WithOne(g => g.Budget)
                .HasForeignKey(g => g.BudgetId)
                .OnDelete(DeleteBehavior.Cascade);
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Budget> Budgets { get; set; }
        public DbSet<FutureSaving> FutureSavings { get; set; }
        public DbSet<FutureTransaction> FutureTransactions { get; set; }
        public DbSet<Goal> Goals { get; set; }
        public DbSet<Saving> Savings { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
    }
}