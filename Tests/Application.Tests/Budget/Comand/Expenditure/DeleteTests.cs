using Application.DailyActions.DailyExpenditures;
using Application.Tests.Common;
using Microsoft.EntityFrameworkCore;
using Shouldly;

namespace Application.Tests.DailyActions.Comand.Expenditure;

public class DeleteTests : CommandTestBase
{
    [Fact]
    public async Task ShouldSuccess()
    {
        // Arrange
        var handler = new Delete.Handler(_context);
        var expenditure = await _context.Transactions.FirstOrDefaultAsync(x => x.Amount < 0);

        // Act
        var result = await handler.Handle(new Delete.Command { ExpenditureId = expenditure.Id },
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
        var result = await handler.Handle(new Delete.Command { ExpenditureId = Guid.Empty},
            CancellationToken.None).ConfigureAwait(false);

        // Assert
        result.ShouldBeNull();
    }
}