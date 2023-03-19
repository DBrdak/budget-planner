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

namespace Application.DailyActions.DailyIncomes;

public class Create
{
    public class Command : IRequest<Result<Unit>>
    {
        public IncomeDto NewIncome { get; set; }
    }

    public class CommandValidator : AbstractValidator<Command>
    {
        private readonly IValidationExtension _validationExtension;

        public CommandValidator(IValidationExtension validationExtension)
        {
            _validationExtension = validationExtension;

            RuleFor(x => x.NewIncome).SetValidator(new IncomeValidator(_validationExtension));
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

        public async Task<Result<Unit>> Handle(Command request,CancellationToken cancellationToken)
        {
            var newIncome = _mapper.Map<Transaction>(request.NewIncome);

            var budgetId = await _budgetAccessor.GetBudgetId().ConfigureAwait(false);

            newIncome.BudgetId = budgetId;

            newIncome.Account = await _context.Accounts
                .FirstOrDefaultAsync(a => a.Name == request.NewIncome.AccountName
                                          && a.BudgetId == budgetId).ConfigureAwait(false);

            newIncome.FutureTransaction = await _context.FutureTransactions
                .FirstOrDefaultAsync(ft => ft.Category == request.NewIncome.Category
                                           && ft.BudgetId == budgetId && ft.Amount > 0).ConfigureAwait(false);

            await _context.Transactions.AddAsync(newIncome).ConfigureAwait(false);
            var fail = await _context.SaveChangesAsync().ConfigureAwait(false) < 0;

            if (fail)
                return Result<Unit>.Failure("Problem while adding new income");

            return Result<Unit>.Success(Unit.Value);
        }
    }
}