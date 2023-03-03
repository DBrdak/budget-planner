using Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
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
            //new AccountConfiguration().Configure(builder.Entity<Account>());
            //new BudgetConfiguration().Configure(builder.Entity<Budget>());
            //new FutureSavingConfiguration().Configure(builder.Entity<FutureSaving>());
            //new FutureTransactionConfiguration().Configure(builder.Entity<FutureTransaction>());
            //new GoalConfiguration().Configure(builder.Entity<Goal>());
            //new SavingConfiguration().Configure(builder.Entity<Saving>());
            //new TransactionConfiguration().Configure(builder.Entity<Transaction>());

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
    }
}