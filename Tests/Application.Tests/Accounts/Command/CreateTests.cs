using Application.Accounts;
using Application.DTO;
using Application.Tests.Common;
using Microsoft.EntityFrameworkCore;
using Shouldly;

namespace Application.Tests.Account;

public class CreateTests : CommandTestBase
{
    [Fact]
    public async Task ShouldCreateAccount()
    {
        //Arrange
        var budget = await _context.Budgets.FirstAsync();


        var account = new AccountDto
        {
            Id = Guid.NewGuid(),
            Name = "Test",
            AccountType = "Checking",
            Balance = 1000
        };

        var handler = new Create.Handler(_context, _mapper, _budgetAccessorMock.Object);

        //Act
        var result = await handler.Handle
            (new Create.Command { NewAccount = account }, CancellationToken.None);

        //Assert
        var accountInDb = await _context.Accounts.FindAsync(account.Id);

        result.IsSuccess.ShouldBe(true);
        accountInDb.ShouldNotBeNull();
        //accountInDb.Name.ShouldBe(accountToCreate.Name);
        //accountInDb.AccountType.ShouldBe(accountToCreate.AccountType);
        //accountInDb.Balance.ShouldBe(accountToCreate.Balance);
        //accountInDb.Budget.ShouldBe(budget);
    }
}