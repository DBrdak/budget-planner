using Application.Core;
using Application.Interfaces;
using Application.Tests.Common.DataContextBase;
using AutoMapper;
using Moq;
using Persistence;

namespace Application.Tests.Common.TestBase;

public class QueryTestFixture : IDisposable
{
    protected readonly Mock<IBudgetAccessor> _budgetAccessorMock;
    protected readonly DataContext _context;
    protected readonly IMapper _mapper;

    public QueryTestFixture()
    {
        var configurationProvider = new MapperConfiguration(cfg => { cfg.AddProfile<Core.MappingProfiles>(); });

        _mapper = configurationProvider.CreateMapper();
        _context = DataContextFactory.Create();
        _budgetAccessorMock = new Mock<IBudgetAccessor>();
        SetupBudgetAccessorMock();
    }

    public void Dispose()
    {
        DataContextFactory.Destroy(_context);
    }

    private void SetupBudgetAccessorMock()
    {
        var budget = _context.Budgets.FirstOrDefault();
        _budgetAccessorMock.Setup(x => x.GetBudget()).ReturnsAsync(budget);
        _budgetAccessorMock.Setup(x => x.GetBudgetId()).ReturnsAsync(budget.Id);
    }
}

[CollectionDefinition("QueryCollection")]
public class QueryCollection : ICollectionFixture<QueryTestFixture>
{
}