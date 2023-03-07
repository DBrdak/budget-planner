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

            // Podpowiedzi odnoszą się też do create income

            async Task<Result<Unit>> IRequestHandler<Command, Result<Unit>>.Handle(Command request, CancellationToken cancellationToken)
            {
                Transaction newExpenditure = new Transaction();

                var budgetId = await _budgetAccessor.GetBudgetId();

                newExpenditure.FutureTransaction = await _context.FutureTransactions.FirstOrDefaultAsync(t => t.Category == request.NewExpenditure.Category && t.BudgetId== budgetId);

                if (newExpenditure.Budget == null
                    || newExpenditure.Account == null)
                    return null;

                await _context.Transactions.AddAsync(newExpenditure);

                var fail = await _context.SaveChangesAsync() < 0;

                if (fail)
                    return Result<Unit>.Failure("Problem while adding new expenditure");

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}