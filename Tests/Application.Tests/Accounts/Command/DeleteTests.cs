using Application.Accounts;
using Application.Tests.Common;
using Microsoft.EntityFrameworkCore;
using Shouldly;

namespace Application.Tests.Account;

public class DeleteTests : CommandTestBase
{
    [Fact]
    public async Task ShouldSuccess()
    {
        //Arrange
        var oldAccount = await _context.Accounts.FirstAsync();
        var handler = new Delete.Handler(_context);

        //Act
        var result = await handler.Handle
            (new Delete.Command { AccountId = oldAccount.Id }, CancellationToken.None);

        //Assert
        result.IsSuccess.ShouldBeTrue();
    }

    [Fact]
    public async Task ShouldFail()
    {
        //Arrange
        var handler = new Delete.Handler(_context);

        //Act
        var result = await handler.Handle
            (new Delete.Command { AccountId = Guid.NewGuid() }, CancellationToken.None);

        //Assert
        result.ShouldBeNull();
    }
}