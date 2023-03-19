using Application.Tests.Common;
using Microsoft.EntityFrameworkCore;
using Application.DailyActions.DailySavings;
using Shouldly;

namespace Application.Tests.DailyActions.Comand.Saving;

public class DeleteTests : CommandTestBase
{
    [Fact]
    public async Task ShouldSuccess()
    {
        // Arrange
        var handler = new Delete.Handler(_context);
        var saving = await _context.Savings.FirstOrDefaultAsync();

        // Act
        var result = await handler.Handle(new Delete.Command { SavingId = saving.Id },
            CancellationToken.None).ConfigureAwait(false);

        // Assert
        result.IsSuccess.ShouldBeTrue();
    }

    [Fact]
    public async Task ShouldFail()
    {
        // Arrange
        var handler = new Delete.Handler(_context);

        // Act
        var result = await handler.Handle(new Delete.Command { SavingId = Guid.Empty },
            CancellationToken.None).ConfigureAwait(false);

        // Assert
        result.ShouldBeNull();
    }
}