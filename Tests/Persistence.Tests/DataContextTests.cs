using Domain;
using Microsoft.EntityFrameworkCore;
using Persistence.Tests.Common;
using Shouldly;

namespace Persistence.Tests
{
    public class DataContextTests
    {
        private DataContext _context;

        public DataContextTests()
        {
            _context = DataContextFactory.Create();
        }

        [Fact]
        public async Task ShouldAddToDatabase()
        {
            //Arrange
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
            var goal = await _context.Goals.FirstOrDefaultAsync();

            //Act
            _context.Goals.Remove(goal);
            await _context.SaveChangesAsync();
            goal = await _context.Goals.FindAsync(goal.Id);

            //Assert
            goal.ShouldBeNull();
        }

        [Fact]
        public async Task ShouldDeleteBudgetCascade()
        {
            //Arrange
            var account = await _context.Accounts.FirstOrDefaultAsync();

            //Act
            _context.Accounts.Remove(account);
            await _context.SaveChangesAsync();
            account = await _context.Accounts.FindAsync(account.Id);

            //Assert
            account.ShouldBeNull();
            // Add more logic for finding if related objects behaviour was proper
        }
    }
}