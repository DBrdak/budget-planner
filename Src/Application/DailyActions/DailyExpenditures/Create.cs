using Application.Core;
using Application.Interfaces;
using Application.DTO;
using Domain;
using MediatR;
using Persistence;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using FluentValidation;

namespace Application.DailyActions.DailyExpenditures
{
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
            private readonly DataContext _context;
            private readonly IMapper _mapper;
            private readonly IBudgetAccessor _budgetAccessor;

            public Handler(DataContext context, IMapper mapper, IBudgetAccessor budgetAccessor)
            {
                _context = context;
                _mapper = mapper;
                _budgetAccessor = budgetAccessor;
            }

            async Task<Result<Unit>> IRequestHandler<Command, Result<Unit>>.Handle(Command request, CancellationToken cancellationToken)
            {
                var newExpenditure = _mapper.Map<Transaction>(request.NewExpenditure);

                var budgetId = await _budgetAccessor.GetBudgetId();

                newExpenditure.BudgetId = budgetId;

                newExpenditure.Account = await _context.Accounts
                    .FirstOrDefaultAsync(a => a.Name == request.NewExpenditure.AccountName
                        && a.BudgetId == budgetId);

                newExpenditure.FutureTransaction = await _context.FutureTransactions
                    .FirstOrDefaultAsync(ft => ft.Category == request.NewExpenditure.Category
                        && ft.BudgetId == budgetId && ft.Amount < 0);

                await _context.Transactions.AddAsync(newExpenditure);
                var fail = await _context.SaveChangesAsync() < 0;

                if (fail)
                    return Result<Unit>.Failure("Problem while adding new expenditure");

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}