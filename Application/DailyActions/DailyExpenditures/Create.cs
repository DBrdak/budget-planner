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

namespace Application.DailyActions.DailyExpenditures
{
    public class Create
    {
        public class Command : IRequest<Result<Unit>>
        {
            public ExpenditureDto NewExpenditure { get; set; }
        }

        //Place for validator

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
                var newExpenditure = _mapper.Map<Transaction>(request.NewExpenditure);

                var budgetName = _budgetAccessor.GetBudgetName();

                newExpenditure.Budget = await _context.Budgets
                    .FirstOrDefaultAsync(b => b.Name == budgetName);

                newExpenditure.Account = await _context.Accounts
                    .FirstOrDefaultAsync(a => a.Name == request.NewExpenditure.AccountName
                    && a.Budget.Name == budgetName);

                if (newExpenditure.Budget == null
                    || newExpenditure.Account == null)
                    return null;

                var category = new TransactionCategory
                {
                    Value = newExpenditure.Category,
                    Budget = await _budgetAccessor.GetBudget(),
                    Type = "expenditure"
                };

                await _context.Transactions.AddAsync(newExpenditure);
                await _context.TransactionCategories.AddAsync(category);
                var fail = await _context.SaveChangesAsync() < 0;

                if (fail)
                    return Result<Unit>.Failure("Problem while adding new expenditure");

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}