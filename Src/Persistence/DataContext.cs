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


    /// <summary>
    ///     Saves all changes made in this context to the database.
    ///     Additionally changes existing data when specified tables gets new data.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         This method will automatically call <see cref="M:Microsoft.EntityFrameworkCore.ChangeTracking.ChangeTracker.DetectChanges" />
    ///         to discover any changes to entity instances before saving to the underlying database. This can be disabled via
    ///         <see cref="P:Microsoft.EntityFrameworkCore.ChangeTracking.ChangeTracker.AutoDetectChangesEnabled" />.
    ///     </para>
    ///     <para>
    ///         Entity Framework Core does not support multiple parallel operations being run on the same DbContext instance. This
    ///         includes both parallel execution of async queries and any explicit concurrent use from multiple threads.
    ///         Therefore, always await async calls immediately, or use separate DbContext instances for operations that execute
    ///         in parallel. See <see href="https://aka.ms/efcore-docs-threading">Avoiding DbContext threading issues</see> for more
    ///         information and examples.
    ///     </para>
    ///     <para>
    ///         See <see href="https://aka.ms/efcore-docs-saving-data">Saving data in EF Core</see> for more information and examples.
    ///     </para>
    /// </remarks>
    /// <param name="cancellationToken">A <see cref="T:System.Threading.CancellationToken" /> to observe while waiting for the task to complete.</param>
    /// <returns>
    ///     A task that represents the asynchronous save operation. The task result contains the
    ///     number of state entries written to the database.
    /// </returns>
    /// <exception cref="T:Microsoft.EntityFrameworkCore.DbUpdateException">
    ///     An error is encountered while saving to the database.
    /// </exception>
    /// <exception cref="T:Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException">
    ///     A concurrency violation is encountered while saving to the database.
    ///     A concurrency violation occurs when an unexpected number of rows are affected during save.
    ///     This is usually because the data in the database has been modified since it was loaded into memory.
    /// </exception>
    /// <exception cref="T:System.OperationCanceledException">If the <see cref="T:System.Threading.CancellationToken" /> is canceled.</exception>
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
    {
        var numberOfChanges = ChangeTracker.Entries<FutureTransaction>().Count();

        for (var i = 0; i < numberOfChanges; i++)
        {
            var entry = ChangeTracker.Entries<FutureTransaction>().ElementAtOrDefault(i);

            if (entry != null)
                switch (entry.State)
                {
                    case EntityState.Deleted:
                        var category = TransactionCategories.FirstOrDefault
                            (x => x.BudgetId == entry.Entity.BudgetId && x.Value == entry.Entity.Category);
                        if (category != null) TransactionCategories.Remove(category);
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
                    if (account != null) account.Balance -= amount;
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
                    if (sourceAccount != null) sourceAccount.Balance += amount;
                    if (destinationAccount != null) destinationAccount.Balance -= amount;
                    break;
            }

        return base.SaveChangesAsync(cancellationToken);
    }
}