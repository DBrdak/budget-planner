using Application.Core;
using Application.DTO;
using Application.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Extras.Categories
{
    public class ExpendituresList
    {
        public class Query : IRequest<Result<List<TransactionCategoryDto>>>
        {
        }

        public class Handler : IRequestHandler<Query, Result<List<TransactionCategoryDto>>>
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

            async Task<Result<List<TransactionCategoryDto>>> IRequestHandler<Query, Result<List<TransactionCategoryDto>>>.Handle(Query request, CancellationToken cancellationToken)
            {
                var budgetId = _budgetAccessor.GetBudget().Result.Id;

                if (budgetId == Guid.Empty)
                    return null;

                var categories = await _context.TransactionCategories
                    .Where(tc => tc.BudgetId == budgetId && tc.Type == "expenditure")
                    .ProjectTo<TransactionCategoryDto>(_mapper.ConfigurationProvider)
                    .ToListAsync();

                return Result<List<TransactionCategoryDto>>.Success(categories);
            }
        }
    }
}