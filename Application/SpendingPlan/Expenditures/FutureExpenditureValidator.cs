using Application.DTO;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.SpendingPlan.Expenditures
{
    public class FutureExpenditureValidator : AbstractValidator<FutureExpenditureDto>
    {
        public FutureExpenditureValidator()
        {
            RuleFor(x => x.AccountName).NotEmpty()
                .WithMessage("Account name is required");
            RuleFor(x => x.Date).NotEmpty().Must(d => d > DateTime.UtcNow)
                .WithMessage("Pick future date");
            RuleFor(x => x.Amount).Must(a => a > 0)
                .WithMessage("Amount must be greater than 0");
            RuleFor(x => x.Category).NotEmpty();
        }
    }
}