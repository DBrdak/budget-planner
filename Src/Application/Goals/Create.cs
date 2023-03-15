using Application.Core;
using Application.DTO;
using Application.Interfaces;
using AutoMapper;
using Domain;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Persistence;

namespace Application.Goals
{
    public class Create
    {
        public class Command : IRequest<Result<Unit>>
        {
            public GoalDto NewGoal { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            private readonly IValidationExtension _validationExtension;

            public CommandValidator(IValidationExtension validationExtension)
            {
                _validationExtension = validationExtension;
                RuleFor(x => x.NewGoal).SetValidator(new GoalValidator(_validationExtension));
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

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var newGoal = _mapper.Map<Goal>(request.NewGoal);

                newGoal.CurrentAmount = request.NewGoal.CurrentAmount;

                var budgetId = await _budgetAccessor.GetBudgetId();

                newGoal.BudgetId = budgetId;

                if (newGoal.BudgetId == Guid.Empty)
                    return null;

                await _context.Goals.AddAsync(newGoal);
                var fail = await _context.SaveChangesAsync() < 0;

                if (fail)
                    return Result<Unit>.Failure("Problem while adding new account");

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}