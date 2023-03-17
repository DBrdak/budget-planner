using Application.Core;
using Application.Interfaces;
using Application.Tests.Common.DataContextBase;
using AutoMapper;
using Domain;
using Moq;
using Persistence;

namespace Application.Tests.Common
{
    public class CommandTestBase : IDisposable
    {
        protected readonly IMapper _mapper;
        protected readonly Mock<IBudgetAccessor> _budgetAccessorMock;
        protected readonly DataContext _context;

        public CommandTestBase()
        {
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfiles>();
            });

            _mapper = configurationProvider.CreateMapper();
            _context = DataContextFactory.Create();
            _budgetAccessorMock = new Mock<IBudgetAccessor>();
            SetupBudgetAccessorMock();
        }

        private Budget GetBudget() => _context.Budgets.FirstOrDefault();

        private void SetupBudgetAccessorMock() =>
            _budgetAccessorMock.Setup(x => x.GetBudget()).ReturnsAsync(GetBudget());

        public void Dispose()
        {
            DataContextFactory.Destroy(_context);
        }
    }
}