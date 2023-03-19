using Application.Core;
using Application.DTO;
using Application.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.SpendingPlan.Savings;

public class List
{
    public class Query : IRequest<Result<List<FutureSavingDto>>>
    {
    }

    public class Handler : IRequestHandler<Query, Result<List<FutureSavingDto>>>
    {
        private readonly IBudgetAccessor _budgetAccessor;
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public Handler(DataContext context, IBudgetAccessor budgetAccessor, IMapper mapper)
        {
            _context = context;
            _budgetAccessor = budgetAccessor;
            _mapper = mapper;
        }

        public async Task<Result<List<FutureSavingDto>>> Handle(Query request, CancellationToken cancellationToken)
        {
            var budgetId = await _budgetAccessor.GetBudgetId().ConfigureAwait(false);

            if (budgetId == Guid.Empty)
                return null;

            var futureSavings = await _context.FutureSavings
                .AsNoTracking()
                .Where(fs => fs.BudgetId == budgetId)
                .ProjectTo<FutureSavingDto>(_mapper.ConfigurationProvider)
                .ToListAsync().ConfigureAwait(false);

            return Result<List<FutureSavingDto>>.Success(futureSavings);
        }
    }
}