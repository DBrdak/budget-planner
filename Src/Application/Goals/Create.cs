using Application.Core;
using Application.Interfaces;
using Application.DTO;
using Domain;
using MediatR;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using FluentValidation;

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
            private readonly IHttpContextAccessor _httpContext;
            private readonly IBudgetAccessor _budgetAccessor;

            public Handler(DataContext context, IMapper mapper, IHttpContextAccessor httpContext, IBudgetAccessor budgetAccessor)
            {
                _context = context;
                _mapper = mapper;
                _httpContext = httpContext;
                _budgetAccessor = budgetAccessor;
            }

            async Task<Result<Unit>> IRequestHandler<Command, Result<Unit>>.Handle(Command request, CancellationToken cancellationToken)
            {
                var newGoal = _mapper.Map<Goal>(request.NewGoal);

                newGoal.CurrentAmount = request.NewGoal.CurrentAmount;

                var budgetId = await _budgetAccessor.GetBudgetId();

                newGoal.BudgetId = budgetId; //await _context.Budgets.FindAsync(budgetId);

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