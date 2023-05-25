using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Accounts;
using Application.Core;
using Application.DTO;
using Application.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Extras.Accounts
{
    public class CheckingAccounts
    {
        public class Query : IRequest<Result<List<AccountNameDto>>>
        {
        }

        public class Handler : IRequestHandler<Query, Result<List<AccountNameDto>>>
        {
            private readonly IBudgetAccessor _budgetAccessor;
            private readonly DataContext _context;

            public Handler(DataContext context, IBudgetAccessor budgetAccessor)
            {
                _context = context;
                _budgetAccessor = budgetAccessor;
            }

            public async Task<Result<List<AccountNameDto>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var budgetId = await _budgetAccessor.GetBudgetId();

                if (budgetId == Guid.Empty)
                    return null;

                var accountsNames = await _context.Accounts
                    .Where(a => a.BudgetId == budgetId
                                && a.AccountType.ToLower() == "checking")
                    .Select(a => new AccountNameDto(a.Id, a.Name))
                    .AsNoTracking().ToListAsync();

                return Result<List<AccountNameDto>>.Success(accountsNames);
            }
        }
    }
}