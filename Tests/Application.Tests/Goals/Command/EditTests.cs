using Application.DTO;
using Application.Goals;
using Application.Tests.Common;
using Microsoft.EntityFrameworkCore;
using Shouldly;

namespace Application.Tests.Goals.Command;

public class EditTests : CommandTestBase
{
    [Fact]
    public async Task ShouldEditGoal()
    {
        // Arrange
        var oldGoal = await _context.Goals.FirstOrDefaultAsync();
        var budget = await _context.Budgets.FirstOrDefaultAsync();

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
        oldGoal.ShouldNotBeNull();

        oldGoal.Id.ShouldBe(goal.Id);
        oldGoal.Budget.ShouldBe(budget);
        oldGoal.Description.ShouldBe(goal.Description);
        oldGoal.Name.ShouldBe(goal.Name);
        oldGoal.CurrentAmount.ShouldBe(goal.CurrentAmount);
        oldGoal.RequiredAmount.ShouldBe(goal.RequiredAmount);
        oldGoal.EndDate.ShouldBe(goal.EndDate);
    }
}