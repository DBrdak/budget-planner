using Application.Core;
using Application.Interfaces;
using MediatR;
using Persistence;

namespace Application.DailyActions.DailyIncomes
{
    public class Delete
    {
        public class Command : IRequest<Result<Unit>>
        {
            public Guid IncomeId { get; set; }
        }

        //Place for validator

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
                var income = await _context.Transactions.FindAsync(request.IncomeId);

                var budgetId = await _budgetAccessor.GetBudgetId();

                if (income == null
                    || budgetId == Guid.Empty)
                    return null;

                _context.Transactions.Remove(income);

                var fail = await _context.SaveChangesAsync() < 0;

                if (fail)
                    return Result<Unit>.Failure("Problem while saving changes on database");

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}