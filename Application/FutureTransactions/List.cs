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

namespace Application.FutureTransactions
{
    public class List
    {
        public class Query : IRequest<Result<List<FutureTransaction>>>
        {
        }

        public class Handler : IRequestHandler<Query, Result<List<FutureTransaction>>>
        {
            private readonly DataContext _context;
            private readonly IUserAccessor _userAccessor;

            public Handler(DataContext context, IUserAccessor userAccessor)
            {
                _context = context;
                _userAccessor = userAccessor;
            }

            async Task<Result<List<FutureTransaction>>> IRequestHandler<Query, Result<List<FutureTransaction>>>.Handle(Query request, CancellationToken cancellationToken)
            {
                var futureTransactions = await _context.FutureTransactions
                    .AsNoTracking()
                    .Where(ft => ft.Budget.User.UserName == _userAccessor.GetUsername())
                    .ToListAsync();

                return Result<List<FutureTransaction>>.Success(futureTransactions);
            }
        }
    }
}
