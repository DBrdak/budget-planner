﻿using Application.Core;
using Application.DTO;
using Application.Interfaces;
using AutoMapper;
using Domain;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.SpendingPlan.Savings
{
    public class Create
    {
        public class Command : IRequest<Result<Unit>>
        {
            public FutureSavingDto NewFutureSaving { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            private readonly IValidationExtension _validationExtension;

            public CommandValidator(IValidationExtension validationExtension)
            {
                _validationExtension = validationExtension;
                RuleFor(x => x.NewFutureSaving).SetValidator(new FutureSavingValidator(_validationExtension));
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
                var newFutureSaving = _mapper.Map<FutureSaving>(request.NewFutureSaving);

                var budgetId = await _budgetAccessor.GetBudgetId();

                newFutureSaving.BudgetId = budgetId;

                newFutureSaving.FromAccount = await _context.Accounts
                    .FirstOrDefaultAsync(a => a.Name == request.NewFutureSaving.FromAccountName
                    && a.Budget.Id == budgetId);

                newFutureSaving.ToAccount = await _context.Accounts
                    .FirstOrDefaultAsync(a => a.Name == request.NewFutureSaving.ToAccountName
                    && a.Budget.Id == budgetId);

                newFutureSaving.Goal = await _context.Goals
                    .FirstOrDefaultAsync(g => g.Name == request.NewFutureSaving.GoalName
                    && g.Budget.Id == budgetId);

                if (newFutureSaving.BudgetId == Guid.Empty
                    || newFutureSaving.ToAccount == null
                    || newFutureSaving.FromAccount == null)
                    return null;

                await _context.FutureSavings.AddAsync(newFutureSaving);
                var fail = await _context.SaveChangesAsync() < 0;

                if (fail)
                    return Result<Unit>.Failure("Problem while adding new income");

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}