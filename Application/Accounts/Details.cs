using Application.Core;
using Application.Interfaces;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Accounts
{
    public class Details
    {
        public class Query : IRequest<Result<Account>>
        {
            public Guid AccountId { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<Account>>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }

            async Task<Result<Account>> IRequestHandler<Query, Result<Account>>.Handle(Query request, CancellationToken cancellationToken)
            {
                var account = await _context.Accounts
                    .Include(a => a.Transactions)
                    .Include(a => a.SavingsIn)
                    .Include(a => a.SavingsOut)
                    .AsNoTracking()
                    .FirstOrDefaultAsync(a => a.Id == request.AccountId);

                return Result<Account>.Success(account);
            }
        }
    }
}