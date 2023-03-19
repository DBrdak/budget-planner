using Application.Extras.Categories;
using Application.Tests.Common.TestBase;
using Shouldly;

namespace Application.Tests.Extras.Query;

public class ExpenditureListTests : QueryTestFixture
{
    [Fact]
    public async Task ShouldSuccess()
    {
        // Arrange
        var handler = new ExpendituresList.Handler(_context, _budgetAccessorMock.Object, _mapper);

        // Act
        var result = await handler.Handle(new ExpendituresList.Query(), CancellationToken.None);

        // Assert
        result.IsSuccess.ShouldBeTrue();
        result.Value.Count.ShouldBe(2);
    }
}