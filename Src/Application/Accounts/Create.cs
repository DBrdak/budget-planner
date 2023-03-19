using Application.Core;
using Application.DTO;
using Application.Interfaces;
using AutoMapper;
using Domain;
using FluentValidation;
using MediatR;
using Persistence;

namespace Application.Accounts;

public class Create
{
    public class Command : IRequest<Result<Unit>>
    {
        public AccountDto NewAccount { get; set; }
    }

    public class CommandValidator : AbstractValidator<Command>
    {
        private readonly IValidationExtension _validationExtension;

        public CommandValidator(IValidationExtension validationExtension)
        {
            _validationExtension = validationExtension;

            RuleFor(x => x.NewAccount).NotNull()
                .SetValidator(new AccountValidator(_validationExtension));
        }
    }

    public class Handler : IRequestHandler<Command, Result<Unit>>
    {
        private readonly IBudgetAccessor _budgetAccessor;
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public Handler(DataContext context, IMapper mapper, IBudgetAccessor budgetAccessor)
        {
            _context = context;
            _mapper = mapper;
            _budgetAccessor = budgetAccessor;
        }

        public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
        {
            var newAccount = _mapper.Map<Account>(request.NewAccount);

            newAccount.BudgetId = await _budgetAccessor.GetBudgetId();

            if (newAccount.BudgetId == Guid.Empty)
                return null;

            await _context.Accounts.AddAsync(newAccount);
            var fail = await _context.SaveChangesAsync() < 0;

            if (fail)
                return Result<Unit>.Failure("Problem while adding new account");

            return Result<Unit>.Success(Unit.Value);
        }
    }
}