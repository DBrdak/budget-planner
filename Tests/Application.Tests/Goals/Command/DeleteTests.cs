using Application.Goals;
using Application.Tests.Common;
using Microsoft.EntityFrameworkCore;
using Shouldly;

namespace Application.Tests.Goals.Command;

public class DeleteTests : CommandTestBase
{
    [Fact]
    public async Task ShouldSuccess()
    {
        // Arrange
        var goal = await _context.Goals.FirstOrDefaultAsync();
        var handler = new Delete.Handler(_context);

        // Act
        var result = await handler.Handle(new Delete.Command { GoalId = goal.Id }, CancellationToken.None);

        // Assert
        result.IsSuccess.ShouldBeTrue();
    }

    [Fact]
    public async Task ShouldFail()
    {
        // Arrange
        var handler = new Delete.Handler(_context);

        // Act
        var result = await handler.Handle(new Delete.Command { GoalId = Guid.Empty }, CancellationToken.None);

        // Assert
        result.ShouldBeNull();
    }
}