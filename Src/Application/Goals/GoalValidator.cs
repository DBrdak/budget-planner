using Application.DTO;
using Application.Interfaces;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Goals
{
    public class GoalValidator : AbstractValidator<GoalDto>
    {
        private readonly IValidationExtension _validationExtension;

        public GoalValidator(IValidationExtension validationExtension)
        {
            _validationExtension = validationExtension;

            RuleFor(x => x.Name).NotEmpty()
                    .WithMessage("Name is required")
                .MaximumLength(16)
                    .WithMessage("Goal name is too long, maximum length is 16")
                .Must(gn => _validationExtension.UniqueGoalName(gn).Result)
                    .WithMessage(x => $"Goal with name {x.Name} already exists");

            RuleFor(x => x.RequiredAmount).GreaterThan(0)
                .WithMessage("Required amount must be greater than 0");

            RuleFor(x => x.CurrentAmount).GreaterThanOrEqualTo(0)
                .WithMessage("Current amount must be equal or greater than 0");
        }
    }
}