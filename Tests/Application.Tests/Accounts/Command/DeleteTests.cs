using Application.Accounts;
using Application.Tests.Common;
using Application.Tests.Common.TestBase;
using Microsoft.EntityFrameworkCore;
using Shouldly;

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

            result.IsSuccess.ShouldBeTrue();
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
            var accountInDb = await _context.Accounts.FindAsync(oldAccount.Id);
            
            //Assert

            result.ShouldBeNull();
            accountInDb.ShouldNotBeNull();
        }
    }
}