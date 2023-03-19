using Application.DTO;
using Application.Interfaces;
using FluentValidation;

namespace Application.DailyActions.DailySavings;

public class SavingValidator : AbstractValidator<SavingDto>
{
    private readonly IValidationExtension _validationExtension;

    public SavingValidator(IValidationExtension validationExtension)
    {
        _validationExtension = validationExtension;

        RuleFor(x => x.Amount)
            .NotEmpty()
            .WithMessage("Amount is required")
            .GreaterThan(0)
            .WithMessage("Amount must be greater than 0");
        RuleFor(x => x.Date)
            .Must(x => x <= DateTime.UtcNow)
            .WithMessage("Specify past or current date");
        RuleFor(x => x.ToAccountName)
            .Must(x => _validationExtension.AccountExists(x).Result)
            .WithMessage(x => $"Account named {x.ToAccountName} doesn't exists");
        RuleFor(x => x.ToAccountName)
            .Must(x => _validationExtension.AccountTypeOf(x, "Saving").Result)
            .WithMessage(x => $"Account named {x.ToAccountName} doesn't exists");
        RuleFor(x => x.FromAccountName)
            .Must(x => _validationExtension.AccountTypeOf(x, "Checking").Result)
            .WithMessage(x => $"Account named {x.ToAccountName} doesn't exists");
        RuleFor(x => x.GoalName)
            .Must(x => _validationExtension.GoalExists(x).Result || x == null)
            .WithMessage(x => $"Category named {x.GoalName} does't exists");
    }
}