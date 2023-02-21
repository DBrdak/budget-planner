using Application.Core;
using Application.DTO;
using Application.Interfaces;
using Domain;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Application.Profiles
{
    public class UpdatePassword
    {
        public class Command : IRequest<Result<Unit>>
        {
            public PasswordFormDto PasswordForm { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly UserManager<User> _userManager;
            private readonly IUserAccessor _userAccessor;

            public Handler(UserManager<User> userManager, IUserAccessor userAccessor)
            {
                _userManager = userManager;
                _userAccessor = userAccessor;
            }

            async Task<Result<Unit>> IRequestHandler<Command, Result<Unit>>.Handle(Command request, CancellationToken cancellationToken)
            {
                var user = await _userManager.Users.FirstOrDefaultAsync(u => u.UserName == _userAccessor.GetUsername());

                if (user == null)
                    return null;

                var result = await _userManager.ChangePasswordAsync(user, request.PasswordForm.OldPassword, request.PasswordForm.NewPassword);

                if (result.Succeeded == false)
                    return Result<Unit>.Failure("Wrong password");

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}