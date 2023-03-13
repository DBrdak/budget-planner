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
    public class ListTests
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly Mock<IBudgetAccessor> _budgetAccessorMock;

        public ListTests(QueryTestFixture fixture)
        {
            _context = fixture.context;
            _mapper = fixture.mapper;
            _budgetAccessorMock = new Mock<IBudgetAccessor>();
        }

        [Fact]
        public async Task ShouldReturnAccountsList()
        {
            //Arrange
            var budget = await _context.Budgets.FirstAsync();

            var budgetAccessor = _budgetAccessorMock.Setup(x => x.GetBudgetId().Result).Returns(budget.Id);

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