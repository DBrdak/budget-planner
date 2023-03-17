using Domain;
using Microsoft.AspNetCore.Identity;
using Moq;

namespace Persistence.Tests.Common
{
    public class UserCreator
    {
        public static Mock<UserManager<TUser>> MockUserManager<TUser>(TUser user) where TUser : class
        {
            var store = new Mock<IUserStore<TUser>>();
            var mgr = new Mock<UserManager<TUser>>(store.Object, null, null, null, null, null, null, null, null);
            mgr.Object.UserValidators.Add(new UserValidator<TUser>());
            mgr.Object.PasswordValidators.Add(new PasswordValidator<TUser>());

            mgr.Setup(x => x.DeleteAsync(It.IsAny<TUser>())).ReturnsAsync(IdentityResult.Success);
            mgr.Setup(x => x.CreateAsync(It.IsAny<TUser>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success).Callback<TUser, string>((x, y) => user = x);

            return mgr;
        }

        public static async Task<User> CreateTestUser()
        {
            var user = new User
            {
                UserName = "test",
                DisplayName = "Test",
                Email = "test@test.com",
            };

            var userManager = MockUserManager(user).Object;
            await userManager.CreateAsync(user, "Pa$$w0rd");

            return user;
        }
    }
}