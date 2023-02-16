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

namespace Application.FutureSavings
{
    public class List
    {
        public class Query : IRequest<Result<List<FutureSaving>>>
        {
        }

        public class Handler : IRequestHandler<Query, Result<List<FutureSaving>>>
        {
            private readonly DataContext _context;
            private readonly IUserAccessor _userAccessor;

            public Handler(DataContext context, IUserAccessor userAccessor)
            {
                _context = context;
                _userAccessor = userAccessor;
            }

            async Task<Result<List<FutureSaving>>> IRequestHandler<Query, Result<List<FutureSaving>>>.Handle(Query request, CancellationToken cancellationToken)
            {
                var futureSavings = await _context.FutureSavings
                    .AsNoTracking()
                    .Where(fs => fs.Budget.User.UserName == _userAccessor.GetUsername())
                    .ToListAsync();

                return Result<List<FutureSaving>>.Success(futureSavings);
            }
        }
    }
}
