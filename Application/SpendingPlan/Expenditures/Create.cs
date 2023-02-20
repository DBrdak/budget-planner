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
using Persistence.Migrations;
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
            public CommandValidator()
            {
                RuleFor(x => x.NewFutureExpenditure).SetValidator(new FutureTransactionValidator());
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
                var newFutureExpenditure = _mapper.Map<FutureTransaction>(request.NewFutureExpenditure);

                newFutureExpenditure.Budget = await _context.Budgets
                    .FirstOrDefaultAsync(b => b.Name == _budgetAccessor.GetBudgetName());

                newFutureExpenditure.Account = await _context.Accounts
                    .FirstOrDefaultAsync(a => a.Name == request.NewFutureExpenditure.AccountName
                    && a.Budget.Name == _budgetAccessor.GetBudgetName());

                if (newFutureExpenditure.Budget == null || newFutureExpenditure.Account == null)
                    return null;

                await _context.FutureTransactions.AddAsync(newFutureExpenditure);
                var fail = await _context.SaveChangesAsync() < 0;

                if (fail)
                    return Result<Unit>.Failure("Problem while adding new expenditure");

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}