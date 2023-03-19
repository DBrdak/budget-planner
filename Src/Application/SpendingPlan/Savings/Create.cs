using Application.Core;
using Application.DTO;
using Application.Interfaces;
using AutoMapper;
using Domain;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.SpendingPlan.Savings;

public class Create
{
    public class Command : IRequest<Result<Unit>>
    {
        public FutureSavingDto NewFutureSaving { get; set; }
    }

    public class CommandValidator : AbstractValidator<Command>
    {
        private readonly IValidationExtension _validationExtension;

        public CommandValidator(IValidationExtension validationExtension)
        {
            _validationExtension = validationExtension;
            RuleFor(x => x.NewFutureSaving).SetValidator(new FutureSavingValidator(_validationExtension));
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
            var newFutureSaving = _mapper.Map<FutureSaving>(request.NewFutureSaving);

            var budgetId = await _budgetAccessor.GetBudgetId().ConfigureAwait(false);

            newFutureSaving.BudgetId = budgetId;

            newFutureSaving.FromAccount = await _context.Accounts
                .FirstOrDefaultAsync(a => a.Name == request.NewFutureSaving.FromAccountName
                                          && a.Budget.Id == budgetId).ConfigureAwait(false);

            newFutureSaving.ToAccount = await _context.Accounts
                .FirstOrDefaultAsync(a => a.Name == request.NewFutureSaving.ToAccountName
                                          && a.Budget.Id == budgetId).ConfigureAwait(false);

            newFutureSaving.Goal = await _context.Goals
                .FirstOrDefaultAsync(g => g.Name == request.NewFutureSaving.GoalName
                                          && g.Budget.Id == budgetId).ConfigureAwait(false);

            if (newFutureSaving.BudgetId == Guid.Empty
                || newFutureSaving.ToAccount == null
                || newFutureSaving.FromAccount == null)
                return null;

            await _context.FutureSavings.AddAsync(newFutureSaving).ConfigureAwait(false);
            var fail = await _context.SaveChangesAsync().ConfigureAwait(false) < 0;

            if (fail)
                return Result<Unit>.Failure("Problem while adding new income");

            return Result<Unit>.Success(Unit.Value);
        }
    }
}