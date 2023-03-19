using Application.Tests.Common.TestBase;
using Microsoft.EntityFrameworkCore;
using Moq;
using Shouldly;

namespace Application.Tests.Goals.Query;

public class List : QueryTestFixture
{
    [Fact]
    public async Task ShouldSuccess()
    {
        // Arrange
        var handler = new Application.Goals.List.Handler(_context, _budgetAccessorMock.Object, _mapper);

        // Act
        var result = await handler.Handle(new Application.Goals.List.Query(), CancellationToken.None);

        // Assert
        result.IsSuccess.ShouldBeTrue();
    }
}