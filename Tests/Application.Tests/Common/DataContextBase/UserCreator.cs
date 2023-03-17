using Domain;
using Microsoft.AspNetCore.Identity;
using Moq;

namespace Application.Tests.Common.DataContextBase
{
    public class UserCreator
    {
        public static Mock<UserManager<User>> MockUserManager(User user)
        {
            var store = new Mock<IUserStore<User>>();
            var mgr = new Mock<UserManager<User>>(store.Object, null, null, null, null, null, null, null, null);
            mgr.Object.UserValidators.Add(new UserValidator<User>());
            mgr.Object.PasswordValidators.Add(new PasswordValidator<User>());

            mgr.Setup(x => x.DeleteAsync(It.IsAny<User>())).ReturnsAsync(IdentityResult.Success);
            mgr.Setup(x => x.CreateAsync(It.IsAny<User>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success).Callback<User, string>((x, y) => user = x);
            mgr.Setup(x => x.FindByNameAsync(user.UserName)).ReturnsAsync(user);
            mgr.Setup(x => x.CheckPasswordAsync(user, "Pa$$w0rd")).ReturnsAsync(true);

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