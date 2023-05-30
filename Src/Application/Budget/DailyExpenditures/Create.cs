using Application.Core;
using Application.DTO;
using Application.Interfaces;
using AutoMapper;
using Domain;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.DailyActions.DailyExpenditures;

public class Create
{
    public class Command : IRequest<Result<Unit>>
    {
        public ExpenditureDto NewExpenditure { get; set; }
    }

    public class CommandValidator : AbstractValidator<Command>
    {
        private readonly IValidationExtension _validationExtension;

        public CommandValidator(IValidationExtension validationExtension)
        {
            _validationExtension = validationExtension;

            RuleFor(x => x.NewExpenditure).SetValidator(new ExpenditureValidator(_validationExtension));
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

        public async Task<Result<Unit>> Handle(Command request,
            CancellationToken cancellationToken)
        {
            var newExpenditure = _mapper.Map<Transaction>(request.NewExpenditure);

            var budgetId = await _budgetAccessor.GetBudgetId().ConfigureAwait(false);

            newExpenditure.BudgetId = budgetId;

            newExpenditure.Account = await _context.Accounts
                .FirstOrDefaultAsync(a => a.Name == request.NewExpenditure.AccountName
                                          && a.BudgetId == budgetId).ConfigureAwait(false);

            newExpenditure.FutureTransaction = await _context.FutureTransactions
                .FirstOrDefaultAsync(ft => ft.Category == request.NewExpenditure.Category
                                           && ft.BudgetId == budgetId && ft.Amount < 0 &&
                                           ft.Date.Month == newExpenditure.Date.Month
                                           && newExpenditure.Date.Year == ft.Date.Year);

            await _context.Transactions.AddAsync(newExpenditure).ConfigureAwait(false);
            var fail = await _context.SaveChangesAsync().ConfigureAwait(false) < 0;

            if (fail)
                return Result<Unit>.Failure("Problem while adding new expenditure");

            return Result<Unit>.Success(Unit.Value);
        }
    }
}