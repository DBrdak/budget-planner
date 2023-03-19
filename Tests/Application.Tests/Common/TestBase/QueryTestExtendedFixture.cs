using Application.Interfaces;
using Application.Tests.Common.DataContextBase;
using Domain;
using Microsoft.AspNetCore.Identity;
using Moq;

namespace Application.Tests.Common.TestBase;

public class QueryTestExtendedFixture : QueryTestFixture
{
    protected Mock<IUserAccessor> _userAccessorMock;
    protected Mock<UserManager<User>> _userManagerMock;

    public QueryTestExtendedFixture()
    {
        _userAccessorMock = new Mock<IUserAccessor>();
        _userManagerMock = UserCreator.MockUserManager(GetUser());
        SetupUserAccessorMock();
    }

    private User GetUser()
    {
        return _context.Users.FirstOrDefault();
    }

    private void SetupUserAccessorMock()
    {
        _userAccessorMock.Setup(x => x.GetUsername()).Returns(GetUser().UserName);
    }
}