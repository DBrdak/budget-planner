using Application.Core;
using Application.DTO;
using Application.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Accounts;

public class List
{
    public class Query : IRequest<Result<List<AccountDto>>>
    {
    }

    public class Handler : IRequestHandler<Query, Result<List<AccountDto>>>
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

        public async Task<Result<List<AccountDto>>> Handle(Query request, CancellationToken cancellationToken)
        {
            var budgetId = await _budgetAccessor.GetBudgetId();

            if (budgetId == Guid.Empty)
                return null;

            var accounts = await _context.Accounts
                .AsNoTracking()
                .Where(a => a.BudgetId == budgetId)
                .ProjectTo<AccountDto>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return Result<List<AccountDto>>.Success(accounts);
        }
    }
}