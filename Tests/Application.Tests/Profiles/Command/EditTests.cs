using Application.DTO;
using Application.Profiles;
using Application.Tests.Common;
using Microsoft.EntityFrameworkCore;
using Shouldly;

namespace Application.Tests.Profiles.Command;

public class EditTests : CommandTestExtendedBase
{
    [Fact]
    public async Task ShouldUpdateProfile()
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
        var user = await _context.Users.FirstOrDefaultAsync();
        var budget = await _context.Budgets.FirstOrDefaultAsync();

        // Act
        var result = await handler.Handle(new Edit.Command { NewProfile = profileDto }, CancellationToken.None);

        // Assert
        result.IsSuccess.ShouldBeTrue();
        user.DisplayName.ShouldBe(profileDto.DisplayName);
        user.Email.ShouldBe(profileDto.Email);
        user.UserName.ShouldBe(profileDto.Username);
        user.NormalizedEmail.ShouldBe(profileDto.Email.ToUpper());
        user.NormalizedUserName.ShouldBe(profileDto.Username.ToUpper());
        budget.Name.ShouldBe(profileDto.BudgetName);
    }
}