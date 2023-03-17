using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Application.Extras.Categories;
using Application.Tests.Common.TestBase;
using Microsoft.EntityFrameworkCore;
using Moq;
using Shouldly;

namespace Application.Tests.Extras.Query
{
    public class ExpenditureListTests : QueryTestFixture
    {
        [Fact]
        public async Task ShouldReturnListOfExpenditureCategories()
        {
            // Arrange
            var budget = await _context.Budgets.FirstOrDefaultAsync();
            _budgetAccessorMock.Setup(x => x.GetBudgetId()).ReturnsAsync(budget.Id);
            var handler = new ExpendituresList.Handler(_context, _budgetAccessorMock.Object, _mapper);

            // Act
            var result = await handler.Handle(new ExpendituresList.Query(), CancellationToken.None);

            // Assert
            result.IsSuccess.ShouldBeTrue();
            result.Value.Count.ShouldBe(2);
        }
    }
}