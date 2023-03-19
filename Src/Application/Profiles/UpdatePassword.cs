using Application.Core;
using Application.DTO;
using Application.Interfaces;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Profiles;

public class UpdatePassword
{
    public class Command : IRequest<Result<Unit>>
    {
        public PasswordFormDto PasswordForm { get; set; }
    }

    public class Handler : IRequestHandler<Command, Result<Unit>>
    {
        private readonly IUserAccessor _userAccessor;
        private readonly UserManager<User> _userManager;

        public Handler(UserManager<User> userManager, IUserAccessor userAccessor)
        {
            _userManager = userManager;
            _userAccessor = userAccessor;
        }

        public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(_userAccessor.GetUsername());

            if (user == null)
                return null;

            var result = await _userManager.ChangePasswordAsync(user, request.PasswordForm.OldPassword,
                request.PasswordForm.NewPassword);

            if (result.Succeeded == false)
                return Result<Unit>.Failure("Wrong password");

            return Result<Unit>.Success(Unit.Value);
        }
    }
}