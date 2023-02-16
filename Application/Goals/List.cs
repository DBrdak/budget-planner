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

namespace Application.Goals
{
    public class List
    {
        public class Query : IRequest<Result<List<Goal>>>
        {
        }

        public class Handler : IRequestHandler<Query, Result<List<Goal>>>
        {
            private readonly DataContext _context;
            private readonly IUserAccessor _userAccessor;

            public Handler(DataContext context, IUserAccessor userAccessor)
            {
                _context = context;
                _userAccessor = userAccessor;
            }

            async Task<Result<List<Goal>>> IRequestHandler<Query, Result<List<Goal>>>.Handle(Query request, CancellationToken cancellation)
            {
                var goals = await _context.Goals
                    .AsNoTracking()
                    .Where(g => g.Budget.User.UserName == _userAccessor.GetUsername())
                    .ToListAsync();

                return Result<List<Goal>>.Success(goals);
            }
        }
    }
}
