using Application.DTO;
using Domain;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Accounts
{
    public class AccountValidator : AbstractValidator<AccountDto>
    {
        public AccountValidator()
        {
            RuleFor(x => x.AccountType).Must(x => x == "Saving" || x == "Checking")
                .WithMessage("Account type must be one of following: Saving, Checking");
            RuleFor(x => x.Name).NotEmpty()
                .WithMessage("Account name is required")
                .Must(n => n.All(x => char.IsLetterOrDigit(x) || char.IsWhiteSpace(x)))
                    .WithMessage("Only spaces, letters and digits are allowed")
                .MaximumLength(16)
                    .WithMessage("Account name is too long, maximum length is 16");
            RuleFor(x => x.Balance).NotEmpty()
                .WithMessage("Balance is required");
        }
    }
}