using Application.Core;
using Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Accounts
{
    public class Edit
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
            public Handler()
            {
            }

            Task<Result<Unit>> IRequestHandler<Command, Result<Unit>>.Handle(Command request, CancellationToken cancellationToken)
            {
                throw new NotImplementedException();
            }
        }
    }
}