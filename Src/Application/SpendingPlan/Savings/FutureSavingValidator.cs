using Application.DTO;
using Application.Interfaces;
using FluentValidation;

namespace Application.SpendingPlan.Savings
{
    public class FutureSavingValidator : AbstractValidator<FutureSavingDto>
    {
        private readonly IValidationExtension _validationExtension;

        public FutureSavingValidator(IValidationExtension validationExtension)
        {
            _validationExtension = validationExtension;

            RuleFor(x => x.Amount).Must(a => a > 0)
                .WithMessage("Amount must be grater than 0");
            RuleFor(x => x.Date).NotEmpty().Must(d => d > DateTime.UtcNow)
                .WithMessage("Pick future date");
            RuleFor(x => x.FromAccountName)
                .Must(n => _validationExtension.AccountTypeOf(n, "Checking").Result)
                .WithMessage("Wrong account choosen");
            RuleFor(x => x.ToAccountName)
                .Must(n => _validationExtension.AccountTypeOf(n, "Saving").Result)
                .WithMessage("Wrong account choosen");
            RuleFor(x => x.GoalName).Must(gn => _validationExtension.GoalExists(gn).Result)
                .WithMessage(x => $"You don't have any goal that names {x.GoalName}");
        }
    }
}