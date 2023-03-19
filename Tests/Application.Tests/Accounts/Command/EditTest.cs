using Application.Accounts;
using Application.DTO;
using Application.Tests.Common;
using Microsoft.EntityFrameworkCore;
using Shouldly;

namespace Application.Tests.Account;

public class EditTest : CommandTestBase
{
    [Fact]
    public async Task ShouldSuccess()
    {
        //Arrange
        var oldAccount = await _context.Accounts.FirstAsync();
        var handler = new Edit.Handler(_context);

        var account = new AccountDto
        {
            Id = oldAccount.Id,
            Name = "Test",
            AccountType = "Checking",
            Balance = 1000
        };

        //Act
        var result = await handler.Handle
            (new Edit.Command { NewAccount = account }, CancellationToken.None);

        //Assert
        result.IsSuccess.ShouldBeTrue();
    }

    [Fact]
    public async Task ShouldFail()
    {
        //Arrange
        var handler = new Edit.Handler(_context);

        var accountToUpdate = new AccountDto();

        //Act
        var result = await handler.Handle
            (new Edit.Command { NewAccount = accountToUpdate }, CancellationToken.None);

        //Assert
        result.ShouldBeNull();
    }
}