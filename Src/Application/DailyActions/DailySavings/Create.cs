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
using System.Net.Http;
using Application.DailyActions.DailyIncomes;

namespace Application.DailyActions.DailySavings
{
    public class Create
    {
        public class Command : IRequest<Result<Unit>>
        {
            public SavingDto NewSaving { get; set; }
        }

        /*
        public class CommandValidator : AbstractValidator<Command>
        {
            private readonly IValidationExtension _validationExtension;

            public CommandValidator(IValidationExtension validationExtension)
            {
                _validationExtension = validationExtension;

                RuleFor(x => x.NewSaving).SetValidator(new SavingValidator(_validationExtension));
            }
        }
        */
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

                var budgetId = await _budgetAccessor.GetBudgetId();

                newSaving.Budget = await _context.Budgets.FindAsync(budgetId);

                newSaving.Goal = await _context.Goals
                    .FirstOrDefaultAsync(g => g.Name == request.NewSaving.GoalName
                        && g.BudgetId == budgetId);

                newSaving.FromAccount = await _context.Accounts
                    .FirstOrDefaultAsync(a => a.Name == request.NewSaving.ToAccountName
                        && a.BudgetId == budgetId);

                newSaving.ToAccount = await _context.Accounts
                    .FirstOrDefaultAsync(a => a.Name == request.NewSaving.FromAccountName
                        && a.BudgetId == budgetId);

                newSaving.FutureSaving = await _context.FutureSavings
                    .FirstOrDefaultAsync(ft => ft.Budget == newSaving.Budget 
                        && ft.FromAccount == newSaving.FromAccount
                        && ft.ToAccount == newSaving.ToAccount
                        && ft.Goal == newSaving.Goal);

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