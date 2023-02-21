using Application.DTO;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.SpendingPlan
{
    public class FutureIncomeValidator : AbstractValidator<FutureIncomeDto>
    {
        public FutureIncomeValidator()
        {
            RuleFor(x => x.AccountName).NotEmpty()
                .WithMessage("Account name is required");
            RuleFor(x => x.Frequency).Must(f => f == "Weekly" || f == "Monthly" || f == "Quarterly" || f == "Semi-Annually" || f == "Annually")
                .WithMessage("Please choose frequency from list");
            RuleFor(x => x.Date).NotEmpty().Must(d => d > DateTime.UtcNow)
                .WithMessage("Pick future date");
            RuleFor(x => x.Amount).Must(a => a > 0)
                .WithMessage("Amount must be greater than 0");
            RuleFor(x => x.Category).NotEmpty();
        }
    }
}