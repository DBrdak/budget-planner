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

namespace Application.SpendingPlan.Savings
{
    public class List
    {
        public class Query : IRequest<Result<List<FutureSavingDto>>>
        {
        }

        public class Handler : IRequestHandler<Query, Result<List<FutureSavingDto>>>
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

            async Task<Result<List<FutureSavingDto>>> IRequestHandler<Query, Result<List<FutureSavingDto>>>.Handle(Query request, CancellationToken cancellationToken)
            {
                var futureSavings = await _context.FutureSavings
                    .AsNoTracking()
                    .Where(fs => fs.BudgetId == _budgetAccessor.GetBudget().Result.Id)
                    .ProjectTo<FutureSavingDto>(_mapper.ConfigurationProvider)
                    .ToListAsync();

                return Result<List<FutureSavingDto>>.Success(futureSavings);
            }
        }
    }
}