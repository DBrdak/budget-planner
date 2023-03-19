using Application.DailyActions.DailySavings;
using Application.DTO;
using Application.Tests.Common;
using Shouldly;

namespace Application.Tests.DailyActions.Comand.Saving;

public class CreateTests : CommandTestBase
{
    [Fact]
    public async Task ShouldSuccess()
    {
        // Arrange
        var handler = new Create.Handler(_context, _mapper, _budgetAccessorMock.Object);

        var saving = new SavingDto
        {
            FromAccountName = "CheckingTest",
            ToAccountName = "SavingTest",
            Amount = 123,
            Date = DateTime.UtcNow.AddDays(21),
            GoalName = "testGoal"
        };

        // Act
        var result = await handler.Handle(new Create.Command { NewSaving = saving }, CancellationToken.None).ConfigureAwait(false);

        // Assert
        result.IsSuccess.ShouldBeTrue();
    }
}