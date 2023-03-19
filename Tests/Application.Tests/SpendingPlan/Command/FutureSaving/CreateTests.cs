using Application.DTO;
using Application.SpendingPlan.Savings;
using Application.Tests.Common;
using Shouldly;

namespace Application.Tests.SpendingPlan.Command.FutureSaving;

public class CreateTests : CommandTestBase
{
    [Fact]
    public async Task ShouldSuccess()
    {
        // Arrange
        var handler = new Create.Handler(_context, _mapper, _budgetAccessorMock.Object);

        var futureSaving = new FutureSavingDto
        {
            FromAccountName = "CheckingTest",
            ToAccountName = "SavingTest",
            Amount = 123,
            GoalName = "testGoal",
            CompletedAmount = 0,
            Date = DateTime.UtcNow.AddDays(21)
        };

        // Act
        var result = await handler.Handle(new Create.Command { NewFutureSaving = futureSaving },
            CancellationToken.None);

        // Assert
        result.IsSuccess.ShouldBeTrue();
    }

    [Fact]
    public async Task ShouldFail()
    {
        // Arrange
        var handler = new Create.Handler(_context, _mapper, _budgetAccessorMock.Object);

        var futureSaving = new FutureSavingDto();

        // Act
        var result = await handler.Handle(new Create.Command { NewFutureSaving = futureSaving },
            CancellationToken.None);

        // Assert
        result.ShouldBeNull();
    }
}