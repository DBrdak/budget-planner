using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTO;
using Application.Profiles;
using Application.Tests.Common;
using Application.Tests.Common.TestBase;
using Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Moq;
using Shouldly;

namespace Application.Tests.Profiles.Command
{
    public class ChangePasswordTests : CommandTestExtendedBase
    {
        [Fact]
        public async Task ShouldChangePassword()
        {
            // Assert
            var handler = new UpdatePassword.Handler(_userManagerMock.Object, _userAccessorMock.Object);

            var passwordForm = new PasswordFormDto()
            {
                NewPassword = "Pa$$w0rdTest",
                OldPassword = "Pa$$w0rd"
            };

            var identityResult = passwordForm.OldPassword == "Pa$$w0rd"
                ? IdentityResult.Success
                : IdentityResult.Failed();

            _userManagerMock.Setup(x => x.ChangePasswordAsync(It.IsAny<User>(), It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(identityResult);

            // Act
            var result = await handler.Handle(new UpdatePassword.Command { PasswordForm = passwordForm }, CancellationToken.None);

            //Assert
            result.IsSuccess.ShouldBeTrue();
        }
    }
}