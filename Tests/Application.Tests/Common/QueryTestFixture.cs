using Application.Core;
using Application.Interfaces;
using AutoMapper;
using Moq;
using Persistence;

namespace Application.Tests.Common
{
    public class QueryTestFixture : IDisposable
    {
        protected readonly IMapper _mapper;
        protected readonly DataContext _context;
        protected readonly Mock<IBudgetAccessor> _budgetAccessorMock;

        public QueryTestFixture()
        {
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfiles>();
            });

            _mapper = configurationProvider.CreateMapper();
            _context = DataContextFactory.Create();
            _budgetAccessorMock = new Mock<IBudgetAccessor>();
        }

        public void Dispose()
        {
            DataContextFactory.Destroy(_context);
        }
    }

    [CollectionDefinition("QueryCollection")]
    public class QueryCollection : ICollectionFixture<QueryTestFixture>
    { }
}