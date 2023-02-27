using Application.Interfaces;
using Domain;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Validation
{
    public class ValidationExtension : IValidationExtension
    {
        private readonly DataContext _context;
        private readonly IUserAccessor _userAccessor;
        private readonly IBudgetAccessor _budgetAccessor;

        public ValidationExtension(DataContext context, IUserAccessor userAccessor, IBudgetAccessor budgetAccessor)
        {
            _context = context;
            _userAccessor = userAccessor;
            _budgetAccessor = budgetAccessor;
        }

        async Task<bool> IValidationExtension.UniqueBudgetName(string newBudgetName)
        {
            var budgetName = await _budgetAccessor.GetBudgetName();

            return await _context.Budgets
                .Where(b => b.Name != budgetName)
                .AnyAsync(b => b.Name.ToUpper() == newBudgetName.ToUpper());
        }

        async Task<bool> IValidationExtension.UniqueEmail(string newEmail)
        {
            return await _context.Users
                .Where(u => u.UserName != _userAccessor.GetUsername())
                .AnyAsync(u => u.Email == newEmail);
        }

        async Task<bool> IValidationExtension.UniqueUsername(string newUsername)
        {
            return await _context.Users
                .Where(u => u.UserName != _userAccessor.GetUsername())
                .AnyAsync(u => u.UserName == newUsername);
        }

        async Task<bool> IValidationExtension.UniqueCategory(string categoryName, string categoryType)
        {
            var budgetId = await _budgetAccessor.GetBudgetId();

            return !await _context.TransactionCategories
                .Where(tc => tc.Type == categoryType
                    && tc.BudgetId == budgetId)
                .AnyAsync(tc => tc.Value == categoryName);
        }

        async Task<bool> IValidationExtension.AccountExists(string accountName)
        {
            var budgetId = await _budgetAccessor.GetBudgetId();

            return await _context.Accounts
                .Where(a => a.BudgetId == budgetId)
                .AnyAsync(a => a.Name == accountName);
        }
    }
}