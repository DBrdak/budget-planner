using Application.Accounts;
using Application.DTO;
using Application.Tests.Common;
using Microsoft.EntityFrameworkCore;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Tests.Account
{
    public class DeleteTests : CommandTestBase
    {
        [Fact]
        public async Task ShouldDeleteAccount()
        {
            //Arrange
            var oldAccount = await _context.Accounts.FirstAsync();
            var handler = new Delete.Handler(_context);

            //Act
            var result = await handler.Handle
                (new Delete.Command { AccountId = oldAccount.Id }, CancellationToken.None);

            //Assert
            var accountInDb = await _context.Accounts.FindAsync(oldAccount.Id);

            result.IsSuccess.ShouldBe(true);
            accountInDb.ShouldBeNull();
        }

        [Fact]
        public async Task ShouldReturnNull()
        {
            //Arrange
            var oldAccount = await _context.Accounts.FirstAsync();
            var handler = new Delete.Handler(_context);

            //Act
            var result = await handler.Handle
                (new Delete.Command { AccountId = Guid.Empty }, CancellationToken.None);

            //Assert
            var accountInDb = await _context.Accounts.FindAsync(oldAccount.Id);

            result.ShouldBeNull();
            accountInDb.ShouldNotBeNull();
        }
    }
}