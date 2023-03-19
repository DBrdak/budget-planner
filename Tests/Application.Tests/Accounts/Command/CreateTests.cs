using Application.Accounts;
using Application.DTO;
using Application.Tests.Common;
using Microsoft.EntityFrameworkCore;
using Shouldly;

namespace Application.Tests.Account;

public class CreateTests : CommandTestBase
{
    [Fact]
    public async Task ShouldSuccess()
    {
        //Arrange
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
        result.IsSuccess.ShouldBeTrue();
    }

}