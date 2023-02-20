using Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
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