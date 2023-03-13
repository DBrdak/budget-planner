using Application.Core;
using Application.DTO;
using Application.Interfaces;
using AutoMapper;
using FluentValidation;
using MediatR;
using Persistence;

namespace Application.Goals
{
    public class Edit
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

            public Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            async Task<Result<Unit>> IRequestHandler<Command, Result<Unit>>.Handle(Command request, CancellationToken cancellationToken)
            {
                var oldGoal = await _context.Goals.FindAsync(request.NewGoal.Id);

                if (oldGoal == null)
                    return null;

                _mapper.Map(request.NewGoal, oldGoal);

                var fail = await _context.SaveChangesAsync() < 0;

                if (fail)
                    return Result<Unit>.Failure("Problem while updating goal");

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}