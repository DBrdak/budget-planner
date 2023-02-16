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
    public class List
    {
        public class Query : IRequest<Result<List<Account>>>
        {
        }

        public class Handler : IRequestHandler<Query, Result<List<Account>>>
        {
            private readonly DataContext _context;
            private readonly IUserAccessor _userAccessor;

            public Handler(DataContext context, IUserAccessor userAccessor)
            {
                _context = context;
                _userAccessor = userAccessor;
            }

            async Task<Result<List<Account>>> IRequestHandler<Query, Result<List<Account>>>.Handle(Query request, CancellationToken cancellationToken)
            {
                var accounts = await _context.Accounts
                    .AsNoTracking()
                    .Where(a => a.Budget.User.UserName == _userAccessor.GetUsername())
                    .ToListAsync();

                return Result<List<Account>>.Success(accounts);
            }
        }
    }
}