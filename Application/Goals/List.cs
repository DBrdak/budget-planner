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

namespace Application.Goals
{
    public class List
    {
        public class Query : IRequest<Result<List<GoalDto>>>
        {
        }

        public class Handler : IRequestHandler<Query, Result<List<GoalDto>>>
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

            async Task<Result<List<GoalDto>>> IRequestHandler<Query, Result<List<GoalDto>>>.Handle(Query request, CancellationToken cancellation)
            {
                var goals = await _context.Goals
                    .AsNoTracking()
                    .Where(g => g.BudgetId == _budgetAccessor.GetBudget().Result.Id)
                    .ProjectTo<GoalDto>(_mapper.ConfigurationProvider)
                    .ToListAsync();

                return Result<List<GoalDto>>.Success(goals);
            }
        }
    }
}