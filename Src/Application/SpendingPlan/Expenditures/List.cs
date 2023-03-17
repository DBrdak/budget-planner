using Application.Core;
using Application.DTO;
using Application.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.SpendingPlan.Expenditures
{
    public class List
    {
        public class Query : IRequest<Result<List<FutureExpenditureDto>>>
        {
        }

        public class Handler : IRequestHandler<Query, Result<List<FutureExpenditureDto>>>
        {
            private readonly DataContext _context;
            private readonly IBudgetAccessor _budgetAccessor;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IBudgetAccessor budgetAccessor, IMapper mapper)
            {
                _context = context;
                _budgetAccessor = budgetAccessor;
                _mapper = mapper;
            }

            async Task<Result<List<FutureExpenditureDto>>> IRequestHandler<Query, Result<List<FutureExpenditureDto>>>.Handle(Query request, CancellationToken cancellationToken)
            {
                var budgetId = await _budgetAccessor.GetBudgetId();

                if (budgetId == Guid.Empty)
                    return null;

                var futureExpenditures = await _context.FutureTransactions
                    .AsNoTracking()
                    .Where(ft => ft.BudgetId == budgetId && ft.Amount < 0)
                    .ProjectTo<FutureExpenditureDto>(_mapper.ConfigurationProvider)
                    .ToListAsync();

                return Result<List<FutureExpenditureDto>>.Success(futureExpenditures);
            }
        }
    }
}