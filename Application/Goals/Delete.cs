using Application.Core;
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
    public class Delete
    {
        public class Command : IRequest<Result<Unit>>
        {
            public Guid GoalId { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly DataContext _context;

            public Handler(DataContext dataContext)
            {
                _context = dataContext;
            }

            async Task<Result<Unit>> IRequestHandler<Command, Result<Unit>>.Handle(Command request, CancellationToken cancellationToken)
            {
                var account = await _context.Goals
                    .FirstOrDefaultAsync(g => g.Id == request.GoalId);

                if (account == null)
                    return null;

                _context.Goals.Remove(account);

                var fail = await _context.SaveChangesAsync() < 0;

                if(fail)
                    return Result<Unit>.Failure("Problem while saving changes on database");

                return Result<Unit>.Success(Unit.Value);
            }

        }
    }
}
