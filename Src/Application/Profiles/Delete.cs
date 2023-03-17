using Application.Core;
using Application.Interfaces;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Profiles
{
    public class Delete
    {
        public class Command : IRequest<Result<Unit>>
        {
            public string Password { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly DataContext _context;
            private readonly IUserAccessor _userAccessor;
            private readonly UserManager<User> _userManager;

            public Handler(DataContext context, IUserAccessor userAccessor, UserManager<User> userManager)
            {
                _context = context;
                _userAccessor = userAccessor;
                _userManager = userManager;
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var user = await _context.Users
                    .FirstOrDefaultAsync(u => u.UserName == _userAccessor.GetUsername())
                    .ConfigureAwait(false);

                if (user == null)
                    return null;

                var authResult = await _userManager
                    .CheckPasswordAsync(user, request.Password)
                    .ConfigureAwait(true);

                if(!authResult)
                    return Result<Unit>.Failure("Wrong password");

                _context.Remove(user);

                var fail = await _context.SaveChangesAsync().ConfigureAwait(false) < 0;

                if (fail)
                    return Result<Unit>.Failure("Problem while removing user form database");

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}