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

namespace Application.DailyActions.DailyIncomes
{
    public class Create
    {
        public class Command : IRequest<Result<Unit>>
        {
            public IncomeDto NewIncome { get; set; }
        }

        //Place for validator function
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
                var newIncome = _mapper.Map<Transaction>(request.NewIncome);

                var budgetName = await _budgetAccessor.GetBudgetName();

                newIncome.Budget = await _context.Budgets
                    .FirstOrDefaultAsync(b => b.Name == budgetName);

                newIncome.Account = await _context.Accounts
                    .FirstOrDefaultAsync(a => a.Name == request.NewIncome.AccountName
                    && a.Budget.Name == budgetName);

                if (newIncome.Budget == null || newIncome.Account == null)
                    return null;

                var category = new TransactionCategory
                {
                    Value = newIncome.Category,
                    Budget = await _budgetAccessor.GetBudget(),
                    Type = "income"
                };

                await _context.Transactions.AddAsync(newIncome);
                await _context.TransactionCategories.AddAsync(category);

                var fail = await _context.SaveChangesAsync() < 0;

                if (fail)
                    return Result<Unit>.Failure("Problem while adding new income");

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}