using Application.DTO;
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
                .WithMessage("Account type must be one of following: \"Saving\", \"Checking\"");
            RuleFor(x => x.Incomes.Select(i => i.Amount)).Must(x => x.Sum() == 0)
                .WithMessage("You cannot add incomes right now");
            RuleFor(x => x.Expenditures.Select(i => i.Amount)).Must(x => x.Sum() == 0)
                .WithMessage("You cannot add expenditures right now");
            RuleFor(x => x.SavingsIn.Select(i => i.Amount)).Must(x => x.Sum() == 0)
                .WithMessage("You cannot add incoming savings right now");
            RuleFor(x => x.SavingsOut.Select(i => i.Amount)).Must(x => x.Sum() == 0)
                .WithMessage("You cannot add outgoing savings right now");
            RuleFor(x => x.Name).NotEmpty()
                .WithMessage("Name of account is required");
        }
    }
}