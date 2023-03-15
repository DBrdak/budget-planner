using Application.Tests.Common;
using Microsoft.EntityFrameworkCore;
using Moq;
using Shouldly;

namespace Application.Tests.Goals.Query;

public class List : QueryTestFixture
{
    [Fact]
    public async Task ShouldReturnList()
    {
        // Arrange
        var budget = await _context.Budgets.FirstOrDefaultAsync();
        _budgetAccessorMock.Setup(x => x.GetBudgetId()).ReturnsAsync(budget.Id);
        var handler = new Application.Goals.List.Handler(_context, _budgetAccessorMock.Object, _mapper);
        
        // Act
        var result = await handler.Handle(new Application.Goals.List.Query(), CancellationToken.None);
        
        // Assert
        result.IsSuccess.ShouldBeTrue();
        result.Value.Count.ShouldBe(1);
    }
}