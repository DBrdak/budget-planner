using Domain;
using Microsoft.EntityFrameworkCore;
using Persistence.Tests.Common;
using Shouldly;
using DataContextFactory = Persistence.Tests.Common.DataContextFactory;

namespace Persistence.Tests
{
    public class DataContextTests
    {
        // Musimy sprawdziæ czy w wyniku seedowania (patrz -> Common.SeedTestData.cs) wszystko dodaje siê tak jakbyœmy chcieli
        [Fact]
        public async Task ShouldAddToDatabaseWithRelations()
        {
            //Arrange
            var context = DataContextFactory.Create();

            //Act

            //Assert
        }

        // Potrzebujemy testu dla nadpisanej metody SaveChangesAsync

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

                    result = dependentEntities.IfAllEntitiesHasBeenDeleted<Account>(context);

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

                    result = dependentEntities.IfAllEntitiesHasBeenDeleted<Account>(context);

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

                    result = dependentEntities.IfAllEntitiesHasBeenDeleted<Account>(context);

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

                    result = dependentEntities.IfAllEntitiesHasBeenDeleted<Transaction>(context);

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

                    result = dependentEntities.IfAllEntitiesHasBeenDeleted<Transaction>(context);

                    //Assert
                    result.ShouldBeTrue();
                    break;
            }
        }
    }
}