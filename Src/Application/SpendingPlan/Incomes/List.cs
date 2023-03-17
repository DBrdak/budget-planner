using Application.Core;
using Application.DTO;
using Application.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.SpendingPlan.Incomes
{
    public class List
    {
        public class Query : IRequest<Result<List<FutureIncomeDto>>>
        {
        }

        public class Handler : IRequestHandler<Query, Result<List<FutureIncomeDto>>>
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

            async Task<Result<List<FutureIncomeDto>>> IRequestHandler<Query, Result<List<FutureIncomeDto>>>.Handle(Query request, CancellationToken cancellationToken)
            {
                var budgetId = await _budgetAccessor.GetBudgetId();

                if (budgetId == Guid.Empty)
                    return null;

                var futureTransactions = await _context.FutureTransactions
                    .AsNoTracking()
                    .Where(ft => ft.BudgetId == budgetId && ft.Amount > 0)
                    .ProjectTo<FutureIncomeDto>(_mapper.ConfigurationProvider)
                    .ToListAsync();

                return Result<List<FutureIncomeDto>>.Success(futureTransactions);
            }
        }
    }
}