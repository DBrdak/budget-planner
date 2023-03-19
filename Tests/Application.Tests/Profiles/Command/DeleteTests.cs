using Application.Profiles;
using Application.Tests.Common;
using Shouldly;

namespace Application.Tests.Profiles.Command;

public class DeleteTests : CommandTestExtendedBase
{
    [Fact]
    public async Task ShouldSuccess()
    {
        //Arrange
        var handler = new Delete.Handler(_context, _userAccessorMock.Object, _userManagerMock.Object);

        // Act
        var result = await handler.Handle(new Delete.Command { Password = "Pa$$w0rd" }, CancellationToken.None);

        // Assert
        result.IsSuccess.ShouldBeTrue();
        _context.Users.ShouldBeEmpty();
    }

    [Fact]
    public async Task ShouldFail()
    {
        //Arrange
        var handler = new Delete.Handler(_context, _userAccessorMock.Object, _userManagerMock.Object);

        // Act
        var result = await handler.Handle(new Delete.Command { Password = "Pa$" }, CancellationToken.None);

        // Assert
        result.IsSuccess.ShouldBeFalse();
    }
}