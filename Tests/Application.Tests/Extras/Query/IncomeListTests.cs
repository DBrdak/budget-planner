using Application.Extras.Categories;
using Application.Tests.Common.TestBase;
using Shouldly;

namespace Application.Tests.Extras.Query;

public class IncomeListTests : QueryTestFixture
{
    [Fact]
    public async Task ShouldReturnListOfIncomeCategories()
    {
        // Arrange
        var handler = new IncomesList.Handler(_context, _budgetAccessorMock.Object, _mapper);

        // Act
        var result = await handler.Handle(new IncomesList.Query(), CancellationToken.None);

        // Assert
        result.IsSuccess.ShouldBeTrue();
        result.Value.Count.ShouldBe(2);
    }
}