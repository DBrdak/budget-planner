using Application.DTO;
using Application.Interfaces;
using FluentValidation;

namespace Application.Accounts
{
    public class AccountValidator : AbstractValidator<AccountDto>
    {
        private readonly IValidationExtension _validationExtension;

        public AccountValidator(IValidationExtension validationExtension)
        {
            _validationExtension = validationExtension;

            RuleFor(x => x.AccountType).Must(x => x == "Saving" || x == "Checking")
                .WithMessage("Account type must be one of following: Saving, Checking");
            RuleFor(x => x.Name).Must(an => _validationExtension.UniqueAccountName(an).Result)
                .WithMessage(x => $"Account with name {x.Name} already exists")
                .Must(n => n.All(x => char.IsLetterOrDigit(x) || char.IsWhiteSpace(x)))
                    .WithMessage("Only spaces, letters and digits are allowed")
                .MaximumLength(16)
                    .WithMessage("Account name is too long, maximum length is 16");
            RuleFor(x => x.Balance).NotEmpty()
                .WithMessage("Balance is required");
        }
    }
}