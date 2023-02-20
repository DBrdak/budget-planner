using Application.DTO;
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
        public GoalValidator()
        {
            RuleFor(x => x.Name).NotEmpty()
                .WithMessage("Name is required");
            RuleFor(x => x.RequiredAmount).GreaterThan(0)
                .WithMessage("Required amount must be greater than 0");
            RuleFor(x => x.CurrentAmount).GreaterThanOrEqualTo(0)
                .WithMessage("Current amount must be equal or greater than 0");
        }
    }
}