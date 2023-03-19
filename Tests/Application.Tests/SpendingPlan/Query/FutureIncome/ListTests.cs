using Application.SpendingPlan.Incomes;
using Application.Tests.Common.TestBase;
using Microsoft.EntityFrameworkCore;
using Shouldly;

namespace Application.Tests.SpendingPlan.Query.FutureIncome;

public class ListTests : QueryTestFixture
{
    [Fact]
    public async Task ShouldSuccess()
    {
        // Arrange
        var handler = new List.Handler(_context, _budgetAccessorMock.Object, _mapper);

        // Act
        var result = await handler.Handle(new List.Query(), CancellationToken.None);

        // Asser
        result.IsSuccess.ShouldBeTrue();
    }
}