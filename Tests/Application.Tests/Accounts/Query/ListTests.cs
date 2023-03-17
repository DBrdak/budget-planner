using Application.Accounts;
using Application.Core;
using Application.DTO;
using Application.Interfaces;
using Application.Tests.Common;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Moq;
using Persistence;
using Shouldly;

namespace Application.Tests.Account.Query
{
    [Collection("QueryCollection")]
    public class ListTests : QueryTestFixture
    {
        [Fact]
        public async Task ShouldReturnAccountsList()
        {
            //Arrange
            var budget = await _context.Budgets.FirstAsync();

            _budgetAccessorMock.Setup(x => x.GetBudgetId()).ReturnsAsync(budget.Id);

            var handler = new List.Handler(_context, _budgetAccessorMock.Object, _mapper);

            //Act
            var result = await handler.Handle(new List.Query(), CancellationToken.None);

            //Assert
            result.IsSuccess.ShouldBeTrue();
            result.ShouldBeOfType<Result<List<AccountDto>>>();
            result.Value.Count.ShouldBe(2);
        }

        [Fact]
        public async Task ShouldReturnNull()
        {
            //Arrange
            var budgetAccessor = _budgetAccessorMock.Setup(x => x.GetBudgetId().Result).Returns(Guid.Empty);

            var handler = new List.Handler(_context, _budgetAccessorMock.Object, _mapper);

            //Act
            var result = await handler.Handle(new List.Query(), CancellationToken.None);

            //Assert
            result.ShouldBeNull();
        }
    }
}