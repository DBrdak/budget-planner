using Application.Core;
using Application.DTO;
using Application.Interfaces;
using AutoMapper;
using Domain;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.DailyActions.DailySavings;

public class Create
{
    public class Command : IRequest<Result<Unit>>
    {
        public SavingDto NewSaving { get; set; }
    }

    public class CommandValidator : AbstractValidator<Command>
    {
        private readonly IValidationExtension _validationExtension;

        public CommandValidator(IValidationExtension validationExtension)
        {
            _validationExtension = validationExtension;

            RuleFor(x => x.NewSaving).SetValidator(new SavingValidator(_validationExtension));
        }
    }

    public class Handler : IRequestHandler<Command, Result<Unit>>
    {
        private readonly IBudgetAccessor _budgetAccessor;
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public Handler(DataContext context, IMapper mapper, IBudgetAccessor budgetAccessor)
        {
            _context = context;
            _mapper = mapper;
            _budgetAccessor = budgetAccessor;
        }

        public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
        {
            var newSaving = _mapper.Map<Saving>(request.NewSaving);

            var budgetId = await _budgetAccessor.GetBudgetId().ConfigureAwait(false);

            newSaving.Budget = await _context.Budgets.FindAsync(budgetId).ConfigureAwait(false);

            newSaving.Goal = await _context.Goals
                .FirstOrDefaultAsync(g => g.Name == request.NewSaving.GoalName
                                          && g.BudgetId == budgetId).ConfigureAwait(false);

            newSaving.FromAccount = await _context.Accounts
                .FirstOrDefaultAsync(a => a.Name == request.NewSaving.FromAccountName
                                          && a.BudgetId == budgetId).ConfigureAwait(false);

            newSaving.ToAccount = await _context.Accounts
                .FirstOrDefaultAsync(a => a.Name == request.NewSaving.ToAccountName
                                          && a.BudgetId == budgetId).ConfigureAwait(false);

            newSaving.FutureSaving = await _context.FutureSavings
                .FirstOrDefaultAsync(ft => ft.Budget == newSaving.Budget
                                           && ft.Date.Month == request.NewSaving.Date.Month
                                           && ft.Date.Year == request.NewSaving.Date.Year
                                           && ft.FromAccount == newSaving.FromAccount
                                           && ft.ToAccount == newSaving.ToAccount
                                           && ft.Goal == newSaving.Goal);

            if (newSaving.Budget == null
                || newSaving.ToAccount == null
                || newSaving.FromAccount == null)
                return null;

            await _context.Savings.AddAsync(newSaving).ConfigureAwait(false);
            var fail = await _context.SaveChangesAsync().ConfigureAwait(false) < 0;

            if (fail)
                return Result<Unit>.Failure("Problem while adding new saving");

            return Result<Unit>.Success(Unit.Value);
        }
    }
}