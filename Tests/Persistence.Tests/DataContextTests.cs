using Domain;
using Microsoft.EntityFrameworkCore;
using Persistence.Tests.Common;
using Shouldly;
using Xunit;

namespace Persistence.Tests
{
    public class DataContextTests
    {
        [Fact] // Fix it
        public async Task ShouldAddToDatabase()
        {
            //Arrange
            var _context = DataContextFactory.Create();

            var newAccount = new Account
            {
                Id = Guid.NewGuid(),
                Name = "test",
                AccountType = "Checking",
                Balance = 2222,
            };

            //Act
            await _context.Accounts.AddAsync(newAccount);
            await _context.SaveChangesAsync();
            var accountInDb = await _context.Accounts.FindAsync(newAccount.Id);

            //Assert
            accountInDb.ShouldNotBeNull();
            accountInDb.FutureTransactions.ShouldBeEmpty();
            accountInDb.FutureSavingsIn.ShouldBeEmpty();
            accountInDb.FutureSavingsOut.ShouldBeEmpty();
            accountInDb.Transactions.ShouldBeEmpty();
            accountInDb.SavingsIn.ShouldBeEmpty();
            accountInDb.SavingsOut.ShouldBeEmpty();
            accountInDb.BudgetId.ShouldBe(Guid.Empty);
            accountInDb.Budget.ShouldBeNull();
            accountInDb.AccountType.ShouldBe("Checking");
            accountInDb.Balance.ShouldBe(2222);
            accountInDb.Name.ShouldBe("test");
            accountInDb.Id.ShouldBe(newAccount.Id);
            accountInDb.ShouldBeOfType<Account>();
        }

        [Fact]
        public async Task ShouldDeleteFromDatabase()
        {
            //Arrange
            var _context = DataContextFactory.Create();

            var goal = await _context.Goals.FirstOrDefaultAsync();

            //Act
            _context.Goals.Remove(goal);
            await _context.SaveChangesAsync();
            goal = await _context.Goals.FindAsync(goal.Id);

            //Assert
            goal.ShouldBeNull();
        }

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
                    var _context = DataContextFactory.Create();

                    var budget = await _context.Budgets.FirstOrDefaultAsync();

                    //Act
                    var dependentEntities = budget.GetType()
                        .Properties(_context, budget.Id)
                        .RelatedEntities(budget);

                    _context.Remove(budget);
                    await _context.SaveChangesAsync();

                    var result = dependentEntities.IfAllEntitiesHasBeenDeleted<Budget>(_context);

                    //Assert
                    result.ShouldBeTrue();
                    break;

                case "Account":
                    //Arrange
                    _context = DataContextFactory.Create();

                    var account = await _context.Accounts.FirstOrDefaultAsync();

                    //Act
                    dependentEntities = account.GetType()
                        .Properties(_context, account.Id)
                        .RelatedEntities(account);

                    _context.Remove(account);
                    await _context.SaveChangesAsync();

                    result = dependentEntities.IfAllEntitiesHasBeenDeleted<Account>(_context);

                    //Assert
                    result.ShouldBeTrue();
                    break;

                case "FutureSaving":
                    //Arrange
                    _context = DataContextFactory.Create();

                    var futureSaving = await _context.FutureSavings.FirstOrDefaultAsync();

                    //Act
                    dependentEntities = futureSaving.GetType()
                        .Properties(_context, futureSaving.Id)
                        .RelatedEntities(futureSaving);

                    _context.Remove(futureSaving);
                    await _context.SaveChangesAsync();

                    result = dependentEntities.IfAllEntitiesHasBeenDeleted<Account>(_context);

                    //Assert
                    result.ShouldBeTrue();
                    break;

                case "FutureTransaction":
                    //Arrange
                    _context = DataContextFactory.Create();

                    var futureTransaction = await _context.FutureTransactions.FirstOrDefaultAsync();

                    //Act
                    dependentEntities = futureTransaction.GetType()
                        .Properties(_context, futureTransaction.Id)
                        .RelatedEntities(futureTransaction);

                    _context.Remove(futureTransaction);
                    await _context.SaveChangesAsync();

                    result = dependentEntities.IfAllEntitiesHasBeenDeleted<Account>(_context);

                    //Assert
                    result.ShouldBeTrue();
                    break;

                case "Saving":
                    //Arrange
                    _context = DataContextFactory.Create();

                    var saving = await _context.Savings.FirstOrDefaultAsync();

                    //Act
                    dependentEntities = saving.GetType()
                        .Properties(_context, saving.Id)
                        .RelatedEntities(saving);

                    _context.Remove(saving);
                    await _context.SaveChangesAsync();

                    result = dependentEntities.IfAllEntitiesHasBeenDeleted<Account>(_context);

                    //Assert
                    result.ShouldBeTrue();
                    break;

                case "Transaction":
                    //Arrange
                    _context = DataContextFactory.Create();

                    var transaction = await _context.Transactions.FirstOrDefaultAsync();

                    //Act
                    dependentEntities = transaction.GetType()
                        .Properties(_context, transaction.Id)
                        .RelatedEntities(transaction);

                    _context.Remove(transaction);
                    await _context.SaveChangesAsync();

                    result = dependentEntities.IfAllEntitiesHasBeenDeleted<Transaction>(_context);

                    //Assert
                    result.ShouldBeTrue();
                    break;

                case "Goal":
                    //Arrange
                    _context = DataContextFactory.Create();

                    var goal = await _context.Goals.FirstOrDefaultAsync();

                    //Act
                    dependentEntities = goal.GetType()
                        .Properties(_context, goal.Id)
                        .RelatedEntities(goal);

                    _context.Remove(goal);
                    await _context.SaveChangesAsync();

                    result = dependentEntities.IfAllEntitiesHasBeenDeleted<Transaction>(_context);

                    //Assert
                    result.ShouldBeTrue();
                    break;

                case "TransactionCategory":
                    //Arrange
                    _context = DataContextFactory.Create();

                    var category = await _context.TransactionCategories.FirstOrDefaultAsync();

                    //Act
                    dependentEntities = category.GetType()
                        .Properties(_context, category.Id)
                        .RelatedEntities(category);

                    _context.Remove(category);
                    await _context.SaveChangesAsync();

                    result = dependentEntities.IfAllEntitiesHasBeenDeleted<Transaction>(_context);

                    //Assert
                    result.ShouldBeTrue();
                    break;
            }
        }
    }
}