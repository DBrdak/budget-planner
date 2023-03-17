using Application.Accounts;
using Application.DTO;
using Application.Tests.Common;
using Application.Tests.Common.TestBase;
using Microsoft.EntityFrameworkCore;
using Shouldly;

namespace Application.Tests.Account
{
    public class EditTest : CommandTestBase
    {
        [Fact]
        public async Task ShouldUpdateAccount()
        {
            //Arrange
            var budget = await _context.Budgets.FirstAsync();
            var oldAccount = await _context.Accounts.FirstAsync();
            var handler = new Edit.Handler(_context);

            var account = new AccountDto
            {
                Id = oldAccount.Id,
                Name = "Test",
                AccountType = "Checking",
                Balance = 1000,
            };

            //Act
            var result = await handler.Handle
                (new Edit.Command { NewAccount = account }, CancellationToken.None);

            //Assert
            var accountInDb = await _context.Accounts.FindAsync(account.Id);

            result.IsSuccess.ShouldBe(true);
            accountInDb.ShouldNotBeNull();
            accountInDb.Name.ShouldBe(account.Name);
            accountInDb.AccountType.ShouldBe(account.AccountType);
            accountInDb.Balance.ShouldBe(account.Balance);
            accountInDb.Budget.ShouldBe(budget);
        }

        [Fact]
        public async Task ShouldReturnNull()
        {
            //Arrange
            var budget = await _context.Budgets.FirstAsync();
            var oldAccount = await _context.Accounts.FirstAsync();
            var handler = new Edit.Handler(_context);

            var accountToUpdate = new AccountDto();

            //Act
            var result = await handler.Handle
                (new Edit.Command { NewAccount = accountToUpdate }, CancellationToken.None);

            //Assert
            var accountInDb = await _context.Accounts.FindAsync(accountToUpdate.Id);

            result.ShouldBeNull();
            accountInDb.ShouldBeNull();
        }
    }
}