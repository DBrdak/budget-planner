using Application.SpendingPlan.Savings;
using Application.Tests.Common;
using Microsoft.EntityFrameworkCore;
using Shouldly;

namespace Application.Tests.SpendingPlan.Command.FutureSaving;

public class DeleteTests : CommandTestBase
{
    [Fact]
    public async Task ShouldSuccess()
    {
        // Arrange
        var handler = new Delete.Handler(_context);
        var futureSaving = await _context.FutureSavings.FirstOrDefaultAsync();

        // Act
        var result = await handler.Handle(new Delete.Command { FutureSavingId = futureSaving.Id },
            CancellationToken.None).ConfigureAwait(false);

        // Assert
        result.IsSuccess.ShouldBeTrue();
    }
}