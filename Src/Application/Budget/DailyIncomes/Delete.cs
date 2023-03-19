using Application.Core;
using Application.Interfaces;
using MediatR;
using Persistence;

namespace Application.DailyActions.DailyIncomes;

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

        public Handler(DataContext dataContext)
        {
            _context = dataContext;
        }

        public async Task<Result<Unit>> Handle(Command request,CancellationToken cancellationToken)
        {
            var income = await _context.Transactions.FindAsync(request.IncomeId).ConfigureAwait(false);

            if (income == null)
                return null;

            _context.Transactions.Remove(income);

            var fail = await _context.SaveChangesAsync().ConfigureAwait(false) < 0;

            if (fail)
                return Result<Unit>.Failure("Problem while saving changes on database");

            return Result<Unit>.Success(Unit.Value);
        }
    }
}