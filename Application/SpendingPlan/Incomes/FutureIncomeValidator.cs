using Application.DTO;
using Application.Interfaces;
using FluentValidation;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.SpendingPlan
{
    public class FutureIncomeValidator : AbstractValidator<FutureIncomeDto>
    {
        private readonly DataContext _context;
        private readonly IBudgetAccessor _budgetAccessor;

        public FutureIncomeValidator(DataContext context, IBudgetAccessor budgetAccessor)
        {
            _context = context;
            _budgetAccessor = budgetAccessor;
            RuleFor(x => x.AccountName).NotEmpty()
                .WithMessage("Account name is required");
            RuleFor(x => x.Date).NotEmpty().Must(d => d > DateTime.UtcNow)
                .WithMessage("Pick future date");
            RuleFor(x => x.Amount).Must(a => a > 0)
                .WithMessage("Amount must be greater than 0");
            RuleFor(x => x.Category).NotEmpty()
                .WithMessage("Category name is required")
                .MaximumLength(16)
                .WithMessage("Category name is too long, max length is 16")
                .Must(c => !_context.TransactionCategories
                    .Where(tc => tc.Type == "income"
                    && tc.BudgetId == _budgetAccessor.GetBudget().Result.Id)
                    .Any(tc => tc.Value == c))
                .WithMessage(x => $"Category {x.Category} already exists");
        }
    }
}