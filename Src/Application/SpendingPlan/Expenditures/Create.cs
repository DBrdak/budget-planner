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

namespace Application.SpendingPlan.Expenditures
{
    public class Create
    {
        public class Command : IRequest<Result<Unit>>
        {
            public FutureExpenditureDto NewFutureExpenditure { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            private readonly IValidationExtension _validationExtension;

            public CommandValidator(IValidationExtension validationExtension)
            {
                _validationExtension = validationExtension;

                RuleFor(x => x.NewFutureExpenditure).SetValidator(
                    new FutureExpenditureValidator(_validationExtension));
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
                var newFutureExpenditure = _mapper.Map<FutureTransaction>(request.NewFutureExpenditure);

                var budgetId = await _budgetAccessor.GetBudgetId();

                newFutureExpenditure.BudgetId = budgetId;

                newFutureExpenditure.Account = await _context.Accounts
                    .FirstOrDefaultAsync(a => a.Name == request.NewFutureExpenditure.AccountName
                        && a.Budget.Id == budgetId);

                if (newFutureExpenditure.BudgetId == Guid.Empty
                    || newFutureExpenditure.Account == null)
                    return null;

                var category = new TransactionCategory
                {
                    Value = newFutureExpenditure.Category,
                    BudgetId = budgetId,
                    Type = "expenditure"
                };

                await _context.FutureTransactions.AddAsync(newFutureExpenditure);
                await _context.TransactionCategories.AddAsync(category);
                var fail = await _context.SaveChangesAsync() < 0;

                if (fail)
                    return Result<Unit>.Failure("Problem while adding new expenditure");

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}