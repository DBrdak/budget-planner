using Application.Extras.Categories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Shouldly;
using Application.Tests.Common.TestBase;

namespace Application.Tests.Extras.Query
{
    public class IncomeListTests : QueryTestFixture
    {
        [Fact]
        public async Task ShouldReturnListOfIncomeCategories()
        {
            // Arrange
            var budget = await _context.Budgets.FirstOrDefaultAsync();
            _budgetAccessorMock.Setup(x => x.GetBudgetId()).ReturnsAsync(budget.Id);
            var handler = new IncomesList.Handler(_context, _budgetAccessorMock.Object, _mapper);

            // Act
            var result = await handler.Handle(new IncomesList.Query(), CancellationToken.None);

            // Assert
            result.IsSuccess.ShouldBeTrue();
            result.Value.Count.ShouldBe(2);
        }
    }
}