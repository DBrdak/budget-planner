using Application.DTO;
using Application.SpendingPlan.Incomes;
using Application.Tests.Common;
using Microsoft.EntityFrameworkCore;
using Shouldly;

namespace Application.Tests.SpendingPlan.Command.FutureIncome;

public class CreateTests : CommandTestBase
{
    [Fact]
    public async Task ShouldSuccess()
    {
        // Arrange
        var handler = new Create.Handler(_context, _mapper, _budgetAccessorMock.Object);

        var futureIncome = new FutureIncomeDto
        {
            AccountName = "CheckingTest",
            Amount = 123,
            Category = "TestingCategory",
            CompletedAmount = 0,
            Date = DateTime.UtcNow.AddDays(21)
        };

        // Act
        var result = await handler.Handle(new Create.Command { NewFutureIncome = futureIncome },
            CancellationToken.None);

        // Assert
        result.IsSuccess.ShouldBeTrue();
    }

    [Fact]
    public async Task ShouldFail()
    {
        // Arrange
        var handler = new Create.Handler(_context, _mapper, _budgetAccessorMock.Object);

        var futureIncome = new FutureIncomeDto();

        // Act
        var result = await handler.Handle(new Create.Command { NewFutureIncome = futureIncome },
            CancellationToken.None);

        // Assert
        result.ShouldBeNull();
    }
}