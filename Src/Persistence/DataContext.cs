using Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.VisualBasic;
using Persistence.Configurations;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Reflection.Metadata;
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
            builder.ApplyConfigurationsFromAssembly(typeof(FutureSavingConfiguration).Assembly);

            base.OnModelCreating(builder);
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Budget> Budgets { get; set; }
        public DbSet<FutureSaving> FutureSavings { get; set; }
        public DbSet<FutureTransaction> FutureTransactions { get; set; }
        public DbSet<Goal> Goals { get; set; }
        public DbSet<Saving> Savings { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<TransactionCategory> TransactionCategories { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<Transaction>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        var futureTransaction = entry.Entity.FutureTransaction;
                        futureTransaction.CompletedAmount += entry.Entity.Amount;
                        break;

                    case EntityState.Deleted:
                        var futureTransactionId = entry.OriginalValues.Clone().GetValue<Guid>("FutureTransactionId");
                        futureTransaction = entry.Context.Find<FutureTransaction>(futureTransactionId);
                        futureTransaction.CompletedAmount -= entry.OriginalValues.GetValue<double>("Amount");
                        break;
                }
            }

            foreach (var entry in ChangeTracker.Entries<Saving>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        var futureSaving = entry.Entity.FutureSaving;
                        var goal = entry.Entity.Goal;
                        futureSaving.CompletedAmount += entry.Entity.Amount;
                        goal.CurrentAmount += entry.Entity.Amount;
                        break;

                    case EntityState.Deleted:
                        futureSaving = entry.Entity.FutureSaving;
                        goal = entry.Entity.Goal;
                        futureSaving.CompletedAmount -= entry.Entity.Amount;
                        goal.CurrentAmount -= entry.Entity.Amount;
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}