using Application.Core;
using Application.Interfaces;
using Application.DTO;
using Domain;
using MediatR;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using FluentValidation;
using Persistence.Migrations;
using System.Net.Http;

namespace Application.DailyActions.DailySavings
{
    public class Create
    {
        public class Command : IRequest<Result<Unit>>
        {
            public SavingDto NewSaving { get; set; }
        }
        //Place for validator function
        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;
            private readonly IHttpContextAccessor _httpContext;
            private readonly IBudgetAccessor _budgetAccessor;

            public Handler(DataContext context, IMapper mapper, IHttpContextAccessor httpContext, IBudgetAccessor budgetAccessor)
            {
                _context = context;
                _mapper = mapper;
                _httpContext = httpContext;
                _budgetAccessor = budgetAccessor;
            }
            async Task<Result<Unit>> IRequestHandler<Command, Result<Unit>>.Handle(Command request, CancellationToken cancellationToken)
            {
                var newSaving = _mapper.Map<Saving>(request.NewSaving);

                var budgetName = _budgetAccessor.GetBudgetName();

                newSaving.Budget = await _context.Budgets
                    .FirstOrDefaultAsync(b => b.Name == budgetName);

                newSaving.FromAccount = await _context.Accounts
                    .FirstOrDefaultAsync(a => a.Name == request.NewSaving.FromAccountName
                    && a.Budget.Name == budgetName);

                newSaving.ToAccount = await _context.Accounts
                    .FirstOrDefaultAsync(a => a.Name == request.NewSaving.ToAccountName
                    && a.Budget.Name == budgetName);

                newSaving.Goal = await _context.Goals
                    .FirstOrDefaultAsync(g => g.Name == request.NewSaving.GoalName
                    && g.Budget.Name == budgetName);

                if (newSaving.Budget == null
                    || newSaving.ToAccount == null
                    || newSaving.FromAccount == null)
                    return null;

                await _context.Savings.AddAsync(newSaving);
                var fail = await _context.SaveChangesAsync() < 0;

                if (fail)
                    return Result<Unit>.Failure("Problem while adding new saving");

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}
