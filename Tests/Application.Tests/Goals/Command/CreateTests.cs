using Application.DTO;
using Application.Goals;
using Application.Tests.Common;
using Microsoft.EntityFrameworkCore;
using Shouldly;

namespace Application.Tests.Goals.Command;

public class CreateTests : CommandTestBase
{
    [Fact]
    public async Task ShouldSuccess()
    {
        // Arrange

        var goal = new GoalDto
        {
            CurrentAmount = 500,
            RequiredAmount = 1500,
            Name = "New test",
            Description = "Just testing",
            EndDate = DateTime.UtcNow.AddDays(55)
        };

        var handler = new Create.Handler(_context, _mapper, _budgetAccessorMock.Object);

        // Act
        var result = await handler.Handle(new Create.Command { NewGoal = goal }, CancellationToken.None);

        // Assert
        result.IsSuccess.ShouldBeTrue();
    }
}