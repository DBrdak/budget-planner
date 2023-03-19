using Application.Accounts;
using Application.Core;
using Application.DTO;
using Application.Tests.Common.TestBase;
using Microsoft.EntityFrameworkCore;
using Moq;
using Shouldly;

namespace Application.Tests.Account.Query;

[Collection("QueryCollection")]
public class ListTests : QueryTestFixture
{
    [Fact]
    public async Task ShouldSuccess()
    {
        //Arrange
        var handler = new List.Handler(_context, _budgetAccessorMock.Object, _mapper);

        //Act
        var result = await handler.Handle(new List.Query(), CancellationToken.None);

        var savingAccounts = result.Value.Where(x => x.AccountType == "Saving");
        var checkingAccounts = result.Value.Where(x => x.AccountType == "Checking");

        //Assert
        result.IsSuccess.ShouldBeTrue();

        savingAccounts.Select(x => new { x.Incomes, x.Expenditures })
            .ShouldAllBe(x => !x.Expenditures.Any() && !x.Incomes.Any());
        savingAccounts.Select(x => x.SavingsIn).ElementAt(0).ShouldNotBeEmpty();

        checkingAccounts.Select(x => x.SavingsIn).ElementAt(0).ShouldBeEmpty();
        checkingAccounts.Select(x => new { x.Incomes, x.Expenditures, x.SavingsOut })
            .ShouldAllBe(x => x.Expenditures.Any() && x.Incomes.Any() && x.SavingsOut.Any());
    }
}