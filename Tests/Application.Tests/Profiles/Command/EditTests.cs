using Application.DTO;
using Application.Profiles;
using Application.Tests.Common;
using Microsoft.EntityFrameworkCore;
using Shouldly;

namespace Application.Tests.Profiles.Command;

public class EditTests : CommandTestExtendedBase
{
    [Fact]
    public async Task ShouldSuccess()
    {
        // Arrange
        var profileDto = new ProfileDto
        {
            DisplayName = "update",
            Username = "update2",
            Email = "update@test.com",
            BudgetName = "updatebudget"
        };

        var handler = new Edit.Handler(_context, _mapper, _userAccessorMock.Object, _userManagerMock.Object,
            _budgetAccessorMock.Object);

        // Act
        var result = await handler.Handle(new Edit.Command { NewProfile = profileDto }, CancellationToken.None);

        // Assert
        result.IsSuccess.ShouldBeTrue();
    }
}