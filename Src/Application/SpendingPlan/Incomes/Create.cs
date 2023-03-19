using Application.Core;
using Application.DTO;
using Application.Interfaces;
using AutoMapper;
using Domain;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.SpendingPlan.Incomes;

public class Create
{
    public class Command : IRequest<Result<Unit>>
    {
        public FutureIncomeDto NewFutureIncome { get; set; }
    }

    public class CommandValidator : AbstractValidator<Command>
    {
        private readonly IValidationExtension _validationExtension;

        public CommandValidator(IValidationExtension validationExtension)
        {
            _validationExtension = validationExtension;

            RuleFor(x => x.NewFutureIncome).SetValidator(
                new FutureIncomeValidator(_validationExtension));
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
            var newFutureIncome = _mapper.Map<FutureTransaction>(request.NewFutureIncome);

            var budgetId = await _budgetAccessor.GetBudgetId().ConfigureAwait(false);

            newFutureIncome.BudgetId = budgetId;

            newFutureIncome.Account = await _context.Accounts
                .FirstOrDefaultAsync(a => a.Name == request.NewFutureIncome.AccountName
                                          && a.Budget.Id == budgetId).ConfigureAwait(false);

            if (newFutureIncome.BudgetId == Guid.Empty
                || newFutureIncome.Account == null)
                return null;

            await _context.FutureTransactions.AddAsync(newFutureIncome).ConfigureAwait(false);

            var fail = await _context.SaveChangesAsync().ConfigureAwait(false) < 0;

            if (fail)
                return Result<Unit>.Failure("Problem while adding new income");

            return Result<Unit>.Success(Unit.Value);
        }
    }
}