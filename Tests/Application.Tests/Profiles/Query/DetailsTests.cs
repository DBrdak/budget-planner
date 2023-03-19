using Application.Profiles;
using Application.Tests.Common.TestBase;
using Shouldly;

namespace Application.Tests.Profiles.Query;

public class DetailsTests : QueryTestExtendedFixture
{
    [Fact]
    public async Task ShouldReturnProfileDetails()
    {
        // Arrange
        var handler = new Details.Handler(_userAccessorMock.Object, _mapper, _userManagerMock.Object,
            _budgetAccessorMock.Object);

        // Act
        var result = await handler.Handle(new Details.Query(), CancellationToken.None);

        // Assert
        result.IsSuccess.ShouldBeTrue();
    }
}