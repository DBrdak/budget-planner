using Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Persistence.Configurations;

namespace Persistence;


public class DataContext : IdentityDbContext<User>
{
    public DataContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Account> Accounts { get; set; }
    public DbSet<Budget> Budgets { get; set; }
    public DbSet<FutureSaving> FutureSavings { get; set; }
    public DbSet<FutureTransaction> FutureTransactions { get; set; }
    public DbSet<Goal> Goals { get; set; }
    public DbSet<Saving> Savings { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<TransactionCategory> TransactionCategories { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(typeof(FutureSavingConfiguration).Assembly);

        base.OnModelCreating(builder);
    }
    

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
    {
        var numberOfChanges = ChangeTracker.Entries<FutureTransaction>().Count();

        for (var i = 0; i < numberOfChanges; i++)
        {
            var entry = ChangeTracker.Entries<FutureTransaction>().ElementAtOrDefault(i);

            switch (entry.State)
            {
                case EntityState.Deleted:
                    var category = TransactionCategories.FirstOrDefault
                        (x => x.BudgetId == entry.Entity.BudgetId && x.Value == entry.Entity.Category);
                    TransactionCategories.Remove(category);
                    break;

                case EntityState.Added:
                    category = new TransactionCategory
                    {
                        BudgetId = entry.Entity.BudgetId,
                        Value = entry.Entity.Category,
                        Type = entry.Entity.Amount > 0 ? "income" : "expenditure"
                    };
                    ChangeTracker.Context.Attach(category);
                    break;
            }
        }

        foreach (var entry in ChangeTracker.Entries<Transaction>())
            switch (entry.State)
            {
                case EntityState.Added:
                    var futureTransaction = entry.Entity.FutureTransaction;
                    var account = entry.Entity.Account;
                    var amount = entry.Entity.Amount;

                    if (futureTransaction is not null)
                        futureTransaction.CompletedAmount += amount;
                    account.Balance += amount;
                    break;

                case EntityState.Deleted:
                    var futureTransactionId = entry.OriginalValues.Clone().GetValue<Guid>("FutureTransactionId");
                    var accountId = entry.OriginalValues.Clone().GetValue<Guid>("AccountId");
                    amount = entry.OriginalValues.GetValue<decimal>("Amount");

                    futureTransaction = entry.Context.Find<FutureTransaction>(futureTransactionId);
                    account = entry.Context.Find<Account>(accountId);

                    if (futureTransaction is not null)
                        futureTransaction.CompletedAmount -= amount;
                    account.Balance -= amount;
                    break;
            }

        foreach (var entry in ChangeTracker.Entries<Saving>())
            switch (entry.State)
            {
                case EntityState.Added:
                    var futureSaving = entry.Entity.FutureSaving;
                    var goal = entry.Entity.Goal;
                    var sourceAccount = entry.Entity.FromAccount;
                    var destinationAccount = entry.Entity.ToAccount;
                    var amount = entry.Entity.Amount;

                    if (futureSaving is not null)
                        futureSaving.CompletedAmount += amount;
                    if (goal is not null)
                        goal.CurrentAmount += amount;
                    sourceAccount.Balance -= amount;
                    destinationAccount.Balance += amount;
                    break;

                case EntityState.Deleted:
                    var futureSavingId = entry.OriginalValues.Clone().GetValue<Guid>("FutureSavingId");
                    var goalId = entry.OriginalValues.Clone().GetValue<Guid>("GoalId");
                    var sourceAccountId = entry.OriginalValues.Clone().GetValue<Guid>("FromAccountId");
                    var destinationAccountId = entry.OriginalValues.Clone().GetValue<Guid>("ToAccountId");

                    futureSaving = entry.Context.Find<FutureSaving>(futureSavingId);
                    goal = entry.Context.Find<Goal>(goalId);
                    sourceAccount = entry.Context.Find<Account>(sourceAccountId);
                    destinationAccount = entry.Context.Find<Account>(destinationAccountId);

                    amount = entry.OriginalValues.GetValue<decimal>("Amount");
                    if (futureSaving is not null)
                        futureSaving.CompletedAmount -= amount;
                    if (goal is not null)
                        goal.CurrentAmount -= amount;
                    sourceAccount.Balance += amount;
                    destinationAccount.Balance -= amount;
                    break;
            }

        return base.SaveChangesAsync(cancellationToken);
    }
}