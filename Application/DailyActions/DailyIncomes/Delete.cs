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


namespace Application.DailyActions.DailyIncomes
{
    public class Delete
    {
        public class Command : IRequest<Result<Unit>>
        {
            public Guid IncomeId { get; set; }
        }
        //Place for validator

        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly DataContext _context;
            private readonly IBudgetAccessor _budgetAccessor;

            public Handler(DataContext dataContext, IBudgetAccessor budgetAccessor)
            {
                _context = dataContext;
                _budgetAccessor = budgetAccessor;
            }

            async Task<Result<Unit>> IRequestHandler<Command, Result<Unit>>.Handle(Command request, CancellationToken cancellationToken)
            {
                var Income = await _context.Transactions.FindAsync(request.IncomeId);

                if (Income == null)
                    return null;

                var category = await _context.TransactionCategories
                    .FirstOrDefaultAsync(tc => tc.Value == Income.Category
                    && tc.BudgetId == _budgetAccessor.GetBudget().Result.Id);

                _context.Transactions.Remove(Income);
                _context.TransactionCategories.Remove(category);

                var fail = await _context.SaveChangesAsync() < 0;

                if (fail)
                    return Result<Unit>.Failure("Problem while saving changes on database");

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}