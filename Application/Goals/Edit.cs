using Application.Core;
using Application.DTO;
using AutoMapper;
using Domain;
using FluentValidation;
using MediatR;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Goals
{
    public class Edit
    {
        public class Command : IRequest<Result<Unit>>
        {
            public Guid GoalId { get; set; }
            public GoalDto NewGoal { get; set; }
        }

        /*
        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.AccountId).NotEmpty();
                RuleFor(x => x.NewAccount.Balance).NotEmpty();
                RuleFor(x => x.NewAccount.Name).NotEmpty();
            }
        }
        */

        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }

            async Task<Result<Unit>> IRequestHandler<Command, Result<Unit>>.Handle(Command request, CancellationToken cancellationToken)
            {
                var oldGoal = await _context.Goals.FindAsync(request.GoalId);

                if (oldGoal == null)
                    return null;

                oldGoal.Name = request.NewGoal.Name;
                oldGoal.Description = request.NewGoal.Description;
                oldGoal.EndDate = request.NewGoal.EndDate;
                oldGoal.CurrentAmount = request.NewGoal.CurrentAmount;
                oldGoal.RequiredAmount = request.NewGoal.RequiredAmount;

                _context.Goals.Update(oldGoal);

                var fail = await _context.SaveChangesAsync() < 0;

                if (fail)
                    return Result<Unit>.Failure("Problem while updating goal");

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}