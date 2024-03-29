﻿using Application.Core;
using Application.DTO;
using Application.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Goals;

public class List
{
    public class Query : IRequest<Result<List<GoalDto>>>
    {
    }

    public class Handler : IRequestHandler<Query, Result<List<GoalDto>>>
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

        public async Task<Result<List<GoalDto>>> Handle(Query request, CancellationToken cancellation)
        {
            var budgetId = await _budgetAccessor.GetBudgetId();

            if (budgetId == Guid.Empty)
                return null;

            var goals = await _context.Goals
                .AsNoTracking()
                .Where(g => g.BudgetId == budgetId)
                .ProjectTo<GoalDto>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return Result<List<GoalDto>>.Success(goals);
        }
    }
}