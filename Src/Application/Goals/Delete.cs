﻿using Application.Core;
using FluentValidation;
using MediatR;
using Persistence;

namespace Application.Goals
{
    public class Delete
    {
        public class Command : IRequest<Result<Unit>>
        {
            public Guid GoalId { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.GoalId).NotEmpty();
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
                var goal = await _context.Goals.FindAsync(request.GoalId);

                if (goal == null)
                    return null;

                _context.Goals.Remove(goal);

                var fail = await _context.SaveChangesAsync() < 0;

                if (fail)
                    return Result<Unit>.Failure("Problem while saving changes on database");

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}