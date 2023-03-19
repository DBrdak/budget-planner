using Application.SpendingPlan.Incomes;
using Application.Tests.Common;
using Microsoft.EntityFrameworkCore;
using Shouldly;

namespace Application.Tests.SpendingPlan.Command.FutureIncome;

public class DeleteTests : CommandTestBase
{
    [Fact]
    public async Task ShouldSuccess()
    {
        // Arrange
        var handler = new Delete.Handler(_context);
        var futureIncome = await _context.FutureTransactions.FirstOrDefaultAsync(x => x.Amount > 0);

        // Act
        var result = await handler.Handle(new Delete.Command { FutureIncomeId = futureIncome.Id },
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
        var result = await handler.Handle(new Delete.Command { FutureIncomeId = Guid.Empty },
            CancellationToken.None).ConfigureAwait(false);

        // Assert
        result.ShouldBeNull();
    }
}