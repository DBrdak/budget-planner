using Application.Core;
using FluentValidation;
using MediatR;
using Persistence;

namespace Application.SpendingPlan.Expenditures;

public class Delete
{
    public class Command : IRequest<Result<Unit>>
    {
        public Guid FutureExpenditureId { get; set; }
    }

    public class CommandValidator : AbstractValidator<Command>
    {
        public CommandValidator()
        {
            RuleFor(x => x.FutureExpenditureId).NotEmpty();
        }
    }

    public class Handler : IRequestHandler<Command, Result<Unit>>
    {
        private readonly DataContext _context;

        public Handler(DataContext context)
        {
            _context = context;
        }

        public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
        {
            var futureExpenditure = await _context.FutureTransactions
                .FindAsync(request.FutureExpenditureId).ConfigureAwait(false);

            if (futureExpenditure == null)
                return null;

            _context.FutureTransactions.Remove(futureExpenditure);

            var fail = await _context.SaveChangesAsync().ConfigureAwait(false) < 0;

            if (fail)
                return Result<Unit>.Failure("Problem while saving changes on database");

            return Result<Unit>.Success(Unit.Value);
        }
    }
}