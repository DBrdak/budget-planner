using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Application.Tests.Common
{
    public class CommandTestExtendedBase : CommandTestBase
    {
        protected Mock<IUserAccessor> _userAccessorMock;
        protected Mock<UserManager<User>> _userManagerMock;

        public CommandTestExtendedBase()
        {
            _userAccessorMock = new Mock<IUserAccessor>();
            _userManagerMock = UserCreator.MockUserManager(GetUser());
        }

        private User GetUser() => _context.Users.FirstOrDefault();
    }
}