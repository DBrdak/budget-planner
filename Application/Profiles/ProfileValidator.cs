using Application.DTO;
using Application.Interfaces;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Application.Profiles
{
    public class ProfileValidator : AbstractValidator<ProfileDto>
    {
        private readonly IUniqueUser _uniqueUser;

        public ProfileValidator(IUniqueUser _uniqueUser)
        {
            this._uniqueUser = _uniqueUser;

            RuleFor(x => x.Email).EmailAddress()
                .WithMessage("Invalid email")
                .Must((user, cancellation) =>
                    !_uniqueUser.UniqueEmail(user.Email).Result)
                .WithMessage("Email must be unique");

            RuleFor(x => x.Username).NotEmpty()
                .WithMessage("Username is required")
                .MaximumLength(16)
                .WithMessage("Username is too long, maximum length is 16")
                .Must((user, cancellation) =>
                    !_uniqueUser.UniqueUsername(user.Username).Result)
                .WithMessage("Username must be unique");

            RuleFor(x => x.DisplayName).NotEmpty()
                .WithMessage("Display name is required")
                .MaximumLength(16)
                .WithMessage("Display name is too long, maximum length is 16");

            RuleFor(x => x.BudgetName).NotEmpty()
                .WithMessage("Budget name is required")
                .Must(bn => !_uniqueUser.UniqueBudgetName(bn).Result)
                    .WithMessage("Budget name must be unique")
                .MaximumLength(16)
                    .WithMessage("Budget name is too long, maximum length is 16")
                .Must(bn => bn.All(char.IsLetterOrDigit))
                    .WithMessage("Only letters and digits can be used");
        }
    }
}