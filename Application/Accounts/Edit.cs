using Application.Core;
using Application.DTO;
using AutoMapper;
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
    public class Edit
    {
        public class Command : IRequest<Result<Unit>>
        {
            public Guid AccountId { get; set; }
            public AccountDto NewAccount { get; set; }
        }

        // Validation

        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            async Task<Result<Unit>> IRequestHandler<Command, Result<Unit>>.Handle(Command request, CancellationToken cancellationToken)
            {
                var oldAccount = await _context.Accounts.FindAsync(request.AccountId);

                if (oldAccount == null)
                    return null;

                oldAccount.Balance = request.NewAccount.Balance;
                oldAccount.Name = request.NewAccount.Name;

                _context.Accounts.Update(oldAccount);

                var fail = await _context.SaveChangesAsync() < 0;

                if (fail)
                    return Result<Unit>.Failure("Problem while updating account");

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}