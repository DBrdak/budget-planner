using Application.Core;
using Application.DTO;
using Application.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Extras.Categories;

public class IncomesList
{
    public class Query : IRequest<Result<List<TransactionCategoryDto>>>
    {
    }

    public class Handler : IRequestHandler<Query, Result<List<TransactionCategoryDto>>>
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

        public async Task<Result<List<TransactionCategoryDto>>> Handle(Query request,
            CancellationToken cancellationToken)
        {
            var budgetId = await _budgetAccessor.GetBudgetId();

            if (budgetId == Guid.Empty)
                return null;

            var categories = (await _context.TransactionCategories
                .AsNoTracking()
                .Where(tc => tc.BudgetId == budgetId && tc.Type == "income")
                .ProjectTo<TransactionCategoryDto>(_mapper.ConfigurationProvider)
                .ToListAsync()).DistinctBy(c => c.Value).ToList();

            return Result<List<TransactionCategoryDto>>.Success(categories);
        }
    }
}