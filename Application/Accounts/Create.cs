using Application.Core;
using Application.Interfaces;
using Domain;
using MediatR;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Accounts
{
    public class Create
    {
        public class Command : IRequest<Result<Unit>>
        {
            public Guid AccountId { get; set; }

            // Mapped account
            public Account NewAccount { get; set; }
        }

        // Validation

        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly DataContext _context;
            private readonly IUserAccessor _userAccessor;

            public Handler(DataContext context, IUserAccessor userAccessor)
            {
                _context = context;
                _userAccessor = userAccessor;
            }

            Task<Result<Unit>> IRequestHandler<Command, Result<Unit>>.Handle(Command request, CancellationToken cancellationToken)
            {
                return null;
            }
        }
    }
}