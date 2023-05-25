using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Core;
using Application.DTO;
using Application.Interfaces;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Extras.Goals
{
    public class GoalsName
    {
        public class Query : IRequest<Result<List<GoalName>>>
        { }

        public class Handler : IRequestHandler<Query, Result<List<GoalName>>>
        {
            private readonly IBudgetAccessor _budgetAccessor;
            private readonly DataContext _context;

            public Handler(IBudgetAccessor budgetAccessor, DataContext context)
            {
                _budgetAccessor = budgetAccessor;
                _context = context;
            }

            public async Task<Result<List<GoalName>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var budgetId = await _budgetAccessor.GetBudgetId();

                if (budgetId == Guid.Empty)
                    return null;

                var names = await _context.Goals
                    .Where(g => g.BudgetId == budgetId)
                    .Select(g => new GoalName(g.Id, g.Name))
                    .AsNoTracking().ToListAsync();

                return Result<List<GoalName>>.Success(names);
            }
        }
    }
}