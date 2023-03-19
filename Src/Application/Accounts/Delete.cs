using Application.Core;
using FluentValidation;
using MediatR;
using Persistence;

namespace Application.Accounts;

public class Delete
{
    public class Command : IRequest<Result<Unit>>
    {
        public Guid AccountId { get; set; }
    }

    public class CommandValidator : AbstractValidator<Command>
    {
        public CommandValidator()
        {
            RuleFor(x => x.AccountId).NotEmpty();
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
            var account = await _context.Accounts.FindAsync(request.AccountId);

            if (account == null)
                return null;

            _context.Accounts.Remove(account);

            var fail = await _context.SaveChangesAsync() < 0;

            if (fail)
                return Result<Unit>.Failure("Problem while saving changes on database");

            return Result<Unit>.Success(Unit.Value);
        }
    }
}