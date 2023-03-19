using Application.DTO;
using Application.SpendingPlan.Expenditures;
using Application.Tests.Common;
using Shouldly;

namespace Application.Tests.SpendingPlan.Command.FutureExpenditure;

public class CreateTests : CommandTestBase
{
    [Fact]
    public async Task ShouldSuccess()
    {
        // Arrange
        var handler = new Create.Handler(_context, _mapper, _budgetAccessorMock.Object);

        var futureExpenditure = new FutureExpenditureDto
        {
            AccountName = "CheckingTest",
            Amount = 123,
            Category = "TestingCategory",
            CompletedAmount = 50,
            Date = DateTime.UtcNow.AddDays(21)
        };

        // Act
        var result = await handler.Handle(new Create.Command { NewFutureExpenditure = futureExpenditure },
            CancellationToken.None);

        // Assert
        result.IsSuccess.ShouldBeTrue();
    }

    [Fact]
    public async Task ShouldFail()
    {
        // Arrange
        var handler = new Create.Handler(_context, _mapper, _budgetAccessorMock.Object);

        var futureExpenditure = new FutureExpenditureDto();

        // Act
        var result = await handler.Handle(new Create.Command { NewFutureExpenditure = futureExpenditure },
            CancellationToken.None);

        // Assert
        result.ShouldBeNull();
    }
}