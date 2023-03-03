using Application.Core;
using Application.Interfaces;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DailyActions.DailyExpenditures
{
    public class Delete
    {
        public class Command : IRequest<Result<Unit>>
        {
            public Guid ExpenditureId { get; set; }
        }

        //Place for validator

        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly DataContext _context;
            private readonly IBudgetAccessor _budgetAccessor;

            public Handler(DataContext context, IBudgetAccessor budgetAccessor)
            {
                _context = context;
                _budgetAccessor = budgetAccessor;
            }

            // Podpowiedzi odnoszą się też do delete income

            async Task<Result<Unit>> IRequestHandler<Command, Result<Unit>>.Handle(Command request, CancellationToken cancellationToken)
            {
                var expenditure = await _context.Transactions.FindAsync(request.ExpenditureId);

                if (expenditure == null)
                    return null;

                // Nie usuwamy kategorii

                var category = await _context.TransactionCategories
                    .FirstOrDefaultAsync(tc => tc.Value == expenditure.Category
                    && tc.BudgetId == _budgetAccessor.GetBudget().Result.Id);

                // Musisz też odjąć od completed amount (w odpowiednim future transaction) amount z expenditure

                _context.Transactions.Remove(expenditure);
                _context.TransactionCategories.Remove(category);

                var fail = await _context.SaveChangesAsync() < 0;

                if (fail)
                    return Result<Unit>.Failure("Problem while saving changes on database");

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}