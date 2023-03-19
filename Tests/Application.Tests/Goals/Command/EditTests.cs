using Application.DTO;
using Application.Goals;
using Application.Tests.Common;
using Microsoft.EntityFrameworkCore;
using Shouldly;

namespace Application.Tests.Goals.Command;

public class EditTests : CommandTestBase
{
    [Fact]
    public async Task ShouldSuccess()
    {
        // Arrange
        var oldGoal = await _context.Goals.FirstOrDefaultAsync();

        var goal = new GoalDto
        {
            Id = oldGoal.Id,
            Name = "Edit test",
            Description = "Just Testing",
            CurrentAmount = 123456789,
            RequiredAmount = 987654321,
            EndDate = DateTime.Today.AddDays(3650)
        };

        var handler = new Edit.Handler(_context, _mapper);

        // Act
        var result = await handler.Handle(new Edit.Command { NewGoal = goal }, CancellationToken.None);

        // Assert
        result.IsSuccess.ShouldBeTrue();
    }

    [Fact]
    public async Task ShouldFail()
    {
        // Arrange
        var oldGoal = await _context.Goals.FirstOrDefaultAsync();

        var goal = new GoalDto();

        var handler = new Edit.Handler(_context, _mapper);

        // Act
        var result = await handler.Handle(new Edit.Command { NewGoal = goal }, CancellationToken.None);

        // Assert
        result.ShouldBeNull();
    }
}