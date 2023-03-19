using System.Diagnostics.CodeAnalysis;
using Application.DTO;
using Application.Interfaces;
using FluentValidation;

namespace Application.Profiles;

[SuppressMessage("ReSharper", "AsyncConverter.AsyncWait")]
public class ProfileValidator : AbstractValidator<ProfileDto>
{
    private readonly IProfileValidationExtension _validationExtension;

    public ProfileValidator(IProfileValidationExtension validationExtension)
    {
        _validationExtension = validationExtension;

        RuleFor(x => x.Email).EmailAddress()
            .WithMessage("Invalid email")
            .Must(e => _validationExtension.UniqueEmail(e).Result)
            .WithMessage("Email must be unique");

        RuleFor(x => x.Username).NotEmpty()
            .WithMessage("Username is required")
            .MaximumLength(16)
            .WithMessage("Username is too long, maximum length is 16")
            .Must(x =>
                _validationExtension.UniqueUsername(x).Result)
            .WithMessage("Username must be unique");

        RuleFor(x => x.DisplayName).NotEmpty()
            .WithMessage("Display name is required")
            .MaximumLength(16)
            .WithMessage("Display name is too long, maximum length is 16");

        RuleFor(x => x.BudgetName).NotEmpty()
            .WithMessage("Budget name is required")
            .Must(bn => _validationExtension.UniqueBudgetName(bn).Result)
            .WithMessage("Budget name must be unique")
            .MaximumLength(16)
            .WithMessage("Budget name is too long, maximum length is 16")
            .Must(bn => bn.All(char.IsLetterOrDigit))
            .WithMessage("Only letters and digits can be used");
    }
}