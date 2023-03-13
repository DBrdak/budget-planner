using Application.Core;
using Application.Interfaces;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.SpendingPlan.Incomes
{
    public class Delete
    {
        public class Command : IRequest<Result<Unit>>
        {
            public Guid FutureIncomeId { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.FutureIncomeId).NotEmpty();
            }
        }

        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly DataContext _context;
            private readonly IBudgetAccessor _budgetAccessor;

            public Handler(DataContext dataContext, IBudgetAccessor budgetAccessor)
            {
                _context = dataContext;
                _budgetAccessor = budgetAccessor;
            }

            async Task<Result<Unit>> IRequestHandler<Command, Result<Unit>>.Handle(Command request, CancellationToken cancellationToken)
            {
                var futureIncome = await _context.FutureTransactions.FindAsync(request.FutureIncomeId);

                if (futureIncome == null)
                    return null;

                var budgetId = await _budgetAccessor.GetBudgetId();

                var category = await _context.TransactionCategories
                    .FirstOrDefaultAsync(tc => tc.Value == futureIncome.Category
                    && tc.BudgetId == budgetId);

                _context.FutureTransactions.Remove(futureIncome);
                _context.TransactionCategories.Remove(category);

                var fail = await _context.SaveChangesAsync() < 0;

                if (fail)
                    return Result<Unit>.Failure("Problem while saving changes on database");

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}