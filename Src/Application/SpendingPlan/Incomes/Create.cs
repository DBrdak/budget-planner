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
using System.Net.Http;

namespace Application.SpendingPlan.Incomes
{
    public class Create
    {
        public class Command : IRequest<Result<Unit>>
        {
            public FutureIncomeDto NewFutureIncome { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            private readonly IValidationExtension _validationExtension;

            public CommandValidator(IValidationExtension validationExtension)
            {
                _validationExtension = validationExtension;

                RuleFor(x => x.NewFutureIncome).SetValidator(
                    new FutureIncomeValidator(_validationExtension));
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

            async Task<Result<Unit>> IRequestHandler<Command, Result<Unit>>.Handle(Command request, CancellationToken cancellationToken)
            {
                var newFutureIncome = _mapper.Map<FutureTransaction>(request.NewFutureIncome);

                var budgetName = _budgetAccessor.GetBudgetName();

                var budgetId = await _budgetAccessor.GetBudgetId();

                newFutureIncome.BudgetId = budgetId;

                newFutureIncome.Account = await _context.Accounts
                    .FirstOrDefaultAsync(a => a.Name == request.NewFutureIncome.AccountName
                    && a.Budget.Id == budgetId);

                if (newFutureIncome.BudgetId == Guid.Empty
                    || newFutureIncome.Account == null)
                    return null;

                var category = new TransactionCategory
                {
                    Value = newFutureIncome.Category,
                    BudgetId = budgetId,
                    Type = "income"
                };

                await _context.FutureTransactions.AddAsync(newFutureIncome);
                await _context.TransactionCategories.AddAsync(category);

                var fail = await _context.SaveChangesAsync() < 0;

                if (fail)
                    return Result<Unit>.Failure("Problem while adding new income");

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}