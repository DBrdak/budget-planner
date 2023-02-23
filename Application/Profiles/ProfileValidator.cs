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

            RuleFor(x => x.Email).EmailAddress().Must((user, cancellation) =>
            {
                return !_uniqueUser.UniqueEmail(user.Email).Result;
            }).WithMessage("Email must be unique");

            RuleFor(x => x.Username).NotEmpty().Must((user, cancellation) =>
            {
                return !_uniqueUser.UniqueUsername(user.Username).Result;
            }).WithMessage("Username must be unique");

            RuleFor(x => x.DisplayName).NotEmpty();

            RuleFor(x => x.BudgetName).NotEmpty()
                .Must(bn => !_uniqueUser.UniqueBudgetName(bn).Result)
                    .WithMessage("Budget name must be unique")
                .Must(bn => bn.All(char.IsLetterOrDigit) && bn.Length < 16)
                    .WithMessage("Max lenght is 16 and only letters and digits can be used");
        }
    }
}