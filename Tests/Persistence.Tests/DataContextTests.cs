using Domain;
using Microsoft.EntityFrameworkCore;
using Persistence.Tests.Common;
using Shouldly;

namespace Persistence.Tests;

public class DataContextTests
{
    [Theory]
    [InlineData(nameof(Budget))]
    [InlineData(nameof(Account))]
    [InlineData(nameof(FutureSaving))]
    [InlineData(nameof(FutureTransaction))]
    [InlineData(nameof(Saving))]
    [InlineData(nameof(Transaction))]
    [InlineData(nameof(Goal))]
    [InlineData(nameof(TransactionCategory))]
    public async Task ShouldDeleteWithProperConfiguration(string type)
    {
        switch (type)
        {
            case "Budget":
                //Arrange
                var context = DataContextFactory.Create();

                var budget = await context.Budgets.FirstOrDefaultAsync();

                //Act
                var dependentEntities = budget.GetType()
                    .Properties(context, budget.Id)
                    .RelatedEntities(budget);

                context.Remove(budget);
                await context.SaveChangesAsync();

                var result = dependentEntities.IfAllEntitiesHasBeenDeleted<Budget>(context);

                //Assert
                result.ShouldBeTrue();
                break;

            case "Account":
                //Arrange
                context = DataContextFactory.Create();

                var account = await context.Accounts.FirstOrDefaultAsync();

                //Act
                dependentEntities = account.GetType()
                    .Properties(context, account.Id)
                    .RelatedEntities(account);

                context.Remove(account);
                await context.SaveChangesAsync();

                result = dependentEntities.IfAllEntitiesHasBeenDeleted<Account>(context);

                //Assert
                result.ShouldBeTrue();
                break;

            case "FutureSaving":
                //Arrange
                context = DataContextFactory.Create();

                var futureSaving = await context.FutureSavings.FirstOrDefaultAsync();

                //Act
                dependentEntities = futureSaving.GetType()
                    .Properties(context, futureSaving.Id)
                    .RelatedEntities(futureSaving);

                context.Remove(futureSaving);
                await context.SaveChangesAsync();

                result = dependentEntities.IfAllEntitiesHasBeenDeleted<FutureSaving>(context);

                //Assert
                result.ShouldBeTrue();
                break;

            case "FutureTransaction":
                //Arrange
                context = DataContextFactory.Create();

                var futureTransaction = await context.FutureTransactions.FirstOrDefaultAsync();

                //Act
                dependentEntities = futureTransaction.GetType()
                    .Properties(context, futureTransaction.Id)
                    .RelatedEntities(futureTransaction);

                context.Remove(futureTransaction);
                await context.SaveChangesAsync();

                result = dependentEntities.IfAllEntitiesHasBeenDeleted<FutureTransaction>(context,
                    futureTransaction.Category);

                //Assert
                result.ShouldBeTrue();
                break;

            case "Saving":
                //Arrange
                context = DataContextFactory.Create();

                var saving = await context.Savings.FirstOrDefaultAsync();

                //Act
                dependentEntities = saving.GetType()
                    .Properties(context, saving.Id)
                    .RelatedEntities(saving);

                context.Remove(saving);
                await context.SaveChangesAsync();

                result = dependentEntities.IfAllEntitiesHasBeenDeleted<Saving>(context);

                //Assert
                result.ShouldBeTrue();
                break;

            case "Transaction":
                //Arrange
                context = DataContextFactory.Create();

                var transaction = await context.Transactions.FirstOrDefaultAsync();

                //Act
                dependentEntities = transaction.GetType()
                    .Properties(context, transaction.Id)
                    .RelatedEntities(transaction);

                context.Remove(transaction);
                await context.SaveChangesAsync();

                result = dependentEntities.IfAllEntitiesHasBeenDeleted<Transaction>(context);

                //Assert
                result.ShouldBeTrue();
                break;

            case "Goal":
                //Arrange
                context = DataContextFactory.Create();

                var goal = await context.Goals.FirstOrDefaultAsync();

                //Act
                dependentEntities = goal.GetType()
                    .Properties(context, goal.Id)
                    .RelatedEntities(goal);

                context.Remove(goal);
                await context.SaveChangesAsync();

                result = dependentEntities.IfAllEntitiesHasBeenDeleted<Goal>(context);

                //Assert
                result.ShouldBeTrue();
                break;

            case "TransactionCategory":
                //Arrange
                context = DataContextFactory.Create();

                var category = await context.TransactionCategories.FirstOrDefaultAsync();

                //Act
                dependentEntities = category.GetType()
                    .Properties(context, category.Id)
                    .RelatedEntities(category);

                context.Remove(category);
                await context.SaveChangesAsync();

                result = dependentEntities.IfAllEntitiesHasBeenDeleted<TransactionCategory>(context);

                //Assert
                result.ShouldBeTrue();
                break;
        }
    }

    [Fact]
    public async Task ShouldUseOverridenSaveChangesAsync()
    {
        // Arrange
        var context = DataContextFactory.Create();

        var budget = await context.Budgets.FirstOrDefaultAsync();
        var checkingAccount = await context.Accounts.FirstOrDefaultAsync(x => x.AccountType == "Checking");
        var savingAccount = await context.Accounts.FirstOrDefaultAsync(x => x.AccountType == "Saving");
        var futureSaving = await context.FutureSavings.FirstOrDefaultAsync();
        var futureExpenditure = await context.FutureTransactions.FirstOrDefaultAsync(x => x.Amount < 0);
        var futureIncome = await context.FutureTransactions.FirstOrDefaultAsync(x => x.Amount > 0);
        var goal = await context.Goals.FirstOrDefaultAsync();
        var categories = await context.TransactionCategories.ToListAsync();

        var baseCheckBalance = checkingAccount.Balance;
        var baseSavingkBalance = savingAccount.Balance;
        var baseCurrentAmount = goal.CurrentAmount;
        var baseSavingCompletedAmount = futureSaving.CompletedAmount;
        var baseExpenseCompletedAmount = futureExpenditure.CompletedAmount;
        var baseIncomeCompletedAmount = futureIncome.CompletedAmount;

        var newExpenditure = new Transaction
        {
            Account = checkingAccount,
            Amount = 150,
            Budget = budget,
            Category = futureExpenditure.Category,
            Title = "test expenditure",
            Date = DateTime.UtcNow.AddHours(-6),
            FutureTransaction = futureExpenditure
        };

        var newIncome = new Transaction
        {
            Account = checkingAccount,
            Amount = 55,
            Budget = budget,
            Category = futureIncome.Category,
            Title = "test income",
            Date = DateTime.UtcNow.AddHours(-23),
            FutureTransaction = futureIncome
        };

        var newSaving = new Saving
        {
            Budget = budget,
            FutureSaving = futureSaving,
            Amount = 12,
            Date = DateTime.UtcNow.AddDays(-3),
            FromAccount = checkingAccount,
            ToAccount = savingAccount,
            Goal = goal
        };

        //Act
        await context.Savings.AddAsync(newSaving);
        await context.Transactions.AddAsync(newIncome);
        await context.Transactions.AddAsync(newExpenditure);
        await context.SaveChangesAsync();

        //Assert
        checkingAccount.Balance.ShouldBe(baseCheckBalance + newExpenditure.Amount + newIncome.Amount -
                                         newSaving.Amount);
        savingAccount.Balance.ShouldBe(baseSavingkBalance + newSaving.Amount);
        futureSaving.CompletedAmount.ShouldBe(baseSavingCompletedAmount + newSaving.Amount);
        futureExpenditure.CompletedAmount.ShouldBe(baseExpenseCompletedAmount + newExpenditure.Amount);
        futureIncome.CompletedAmount.ShouldBe(baseIncomeCompletedAmount + newIncome.Amount);
        goal.CurrentAmount.ShouldBe(baseCurrentAmount + newSaving.Amount);
        categories.Count.ShouldBe(4);
    }
}