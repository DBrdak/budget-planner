﻿using Application.DTO;
using Application.Interfaces;
using FluentValidation;

namespace Application.DailyActions.DailyIncomes
{
    public class IncomeValidator : AbstractValidator<IncomeDto>
    {
        private readonly IValidationExtension _validationExtension;

        public IncomeValidator(IValidationExtension validationExtension)
        {
            _validationExtension = validationExtension;

            RuleFor(x => x.Title)
                .NotEmpty()
                    .WithMessage("Title is required")
                .MaximumLength(16)
                    .WithMessage("Title maximum length is 16");
            RuleFor(x => x.Amount)
                .NotEmpty()
                    .WithMessage("Amount is required")
                .GreaterThan(0)
                    .WithMessage("Amount must be greater than 0");
            RuleFor(x => x.Date)
                .Must(x => x <= DateTime.UtcNow)
                    .WithMessage("Specify past or current date");
            RuleFor(x => x.AccountName)
                .Must(x => _validationExtension.AccountExists(x).Result)
                    .WithMessage(x => $"Account named {x.AccountName} doesn't exists");
            RuleFor(x => x.Category)
                .Must(x => _validationExtension.CategoryExists(x, "income").Result)
                    .WithMessage(x => $"Category named {x.Category} does't exists");
        }
    }
}