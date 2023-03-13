using Application.Core;
using Application.DTO;
using Application.Interfaces;
using AutoMapper;
using Domain;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.SpendingPlan.Expenditures
{
    public class Create
    {
        public class Command : IRequest<Result<Unit>>
        {
            public FutureExpenditureDto NewFutureExpenditure { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            private readonly IValidationExtension _validationExtension;

            public CommandValidator(IValidationExtension validationExtension)
            {
                _validationExtension = validationExtension;

                RuleFor(x => x.NewFutureExpenditure).SetValidator(
                    new FutureExpenditureValidator(_validationExtension));
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
                var newFutureExpenditure = _mapper.Map<FutureTransaction>(request.NewFutureExpenditure);

                var budgetId = await _budgetAccessor.GetBudgetId();

                newFutureExpenditure.BudgetId = budgetId;

                newFutureExpenditure.Account = await _context.Accounts
                    .FirstOrDefaultAsync(a => a.Name == request.NewFutureExpenditure.AccountName
                        && a.Budget.Id == budgetId);

                if (newFutureExpenditure.BudgetId == Guid.Empty
                    || newFutureExpenditure.Account == null)
                    return null;

                var category = new TransactionCategory
                {
                    Value = newFutureExpenditure.Category,
                    BudgetId = budgetId,
                    Type = "expenditure"
                };

                await _context.FutureTransactions.AddAsync(newFutureExpenditure);
                await _context.TransactionCategories.AddAsync(category);
                var fail = await _context.SaveChangesAsync() < 0;

                if (fail)
                    return Result<Unit>.Failure("Problem while adding new expenditure");

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}