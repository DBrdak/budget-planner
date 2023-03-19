using Application.DailyActions.DailyIncomes;
using Application.Tests.Common;
using Microsoft.EntityFrameworkCore;
using Shouldly;

namespace Application.Tests.DailyActions.Comand.Income;

public class DeleteTests : CommandTestBase
{
    [Fact]
    public async Task ShouldSuccess()
    {
        // Arrange
        var handler = new Delete.Handler(_context);
        var income = await _context.Transactions.FirstOrDefaultAsync(x => x.Amount > 0);

        // Act
        var result = await handler.Handle(new Delete.Command { IncomeId = income.Id },
            CancellationToken.None).ConfigureAwait(false);

        // Assert
        result.IsSuccess.ShouldBeTrue();
    }
}