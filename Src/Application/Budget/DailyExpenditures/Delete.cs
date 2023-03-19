using Application.Core;
using Application.Interfaces;
using MediatR;
using Persistence;

namespace Application.DailyActions.DailyExpenditures;

public class Delete
{
    public class Command : IRequest<Result<Unit>>
    {
        public Guid ExpenditureId { get; set; }
    }

    public class Handler : IRequestHandler<Command, Result<Unit>>
    {
        private readonly DataContext _context;

        public Handler(DataContext context)
        {
            _context = context;
        }

        public async Task<Result<Unit>> Handle(Command request,
            CancellationToken cancellationToken)
        {
            var expenditure = await _context.Transactions.FindAsync(request.ExpenditureId).ConfigureAwait(false);

            if (expenditure == null)
                return null;

            _context.Transactions.Remove(expenditure);

            var fail = await _context.SaveChangesAsync().ConfigureAwait(false) < 0;

            if (fail)
                return Result<Unit>.Failure("Problem while saving changes on database");

            return Result<Unit>.Success(Unit.Value);
        }
    }
}