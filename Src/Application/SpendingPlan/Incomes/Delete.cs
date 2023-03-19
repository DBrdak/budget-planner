using Application.Core;
using FluentValidation;
using MediatR;
using Persistence;

namespace Application.SpendingPlan.Incomes;

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

        public Handler(DataContext dataContext)
        {
            _context = dataContext;
        }

        public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
        {
            var futureIncome = await _context.FutureTransactions
                .FindAsync(request.FutureIncomeId).ConfigureAwait(false);

            if (futureIncome == null)
                return null;

            _context.FutureTransactions.Remove(futureIncome);

            var fail = await _context.SaveChangesAsync().ConfigureAwait(false) < 0;

            if (fail)
                return Result<Unit>.Failure("Problem while saving changes on database");

            return Result<Unit>.Success(Unit.Value);
        }
    }
}