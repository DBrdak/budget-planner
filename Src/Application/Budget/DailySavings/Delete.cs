using Application.Core;
using MediatR;
using Persistence;

namespace Application.DailyActions.DailySavings;

public class Delete
{
    public class Command : IRequest<Result<Unit>>
    {
        public Guid SavingId { get; set; }
    }

    //Place for validator

    public class Handler : IRequestHandler<Command, Result<Unit>>
    {
        private readonly DataContext _context;

        public Handler(DataContext dataContext)
        {
            _context = dataContext;
        }

        public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
        {
            var saving = await _context.Savings.FindAsync(request.SavingId).ConfigureAwait(false);

            if (saving == null)
                return null;

            _context.Savings.Remove(saving);

            var fail = await _context.SaveChangesAsync().ConfigureAwait(false) < 0;

            if (fail)
                return Result<Unit>.Failure("Problem while saving changes on database");

            return Result<Unit>.Success(Unit.Value);
        }
    }
}