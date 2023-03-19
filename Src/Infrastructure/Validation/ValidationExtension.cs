using Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Infrastructure.Validation;
// Export functions to other classes

public class ValidationExtension : IValidationExtension
{
    private readonly IBudgetAccessor _budgetAccessor;
    private readonly DataContext _context;
    private readonly IUserAccessor _userAccessor;

    public ValidationExtension(DataContext context, IUserAccessor userAccessor, IBudgetAccessor budgetAccessor)
    {
        _context = context;
        _userAccessor = userAccessor;
        _budgetAccessor = budgetAccessor;
    }

    public async Task<bool> UniqueCategory(string categoryName, string categoryType)
    {
        var budgetId = await _budgetAccessor.GetBudgetId();

        return !await _context.TransactionCategories
            .AsNoTracking()
            .Where(tc => tc.Type == categoryType
                         && tc.BudgetId == budgetId)
            .AnyAsync(tc => tc.Value == categoryName);
    }

    public async Task<bool> CategoryExists(string categoryName, string categoryType)
    {
        return !await UniqueCategory(categoryName, categoryType);
    }

    public async Task<bool> AccountExists(string accountName)
    {
        var budgetId = await _budgetAccessor.GetBudgetId();

        return await _context.Accounts
            .AsNoTracking()
            .Where(a => a.BudgetId == budgetId)
            .AnyAsync(a => a.Name == accountName);
    }

    public async Task<bool> AccountTypeOf(string accountName, string accountType)
    {
        var budgetId = await _budgetAccessor.GetBudgetId();

        var account = await _context.Accounts
            .AsNoTracking()
            .Where(a => a.BudgetId == budgetId)
            .FirstOrDefaultAsync(a => a.Name == accountName);

        if (account == null)
            return false;

        return account.AccountType == accountType;
    }

    public async Task<bool> GoalExists(string goalName)
    {
        var budgetId = await _budgetAccessor.GetBudgetId();

        return await _context.Goals
            .AsNoTracking()
            .Where(g => g.BudgetId == budgetId)
            .AnyAsync(g => g.Name == goalName);
    }

    public async Task<bool> UniqueGoalName(string goalName)
    {
        var budgetId = await _budgetAccessor.GetBudgetId();

        return !await _context.Goals
            .AsNoTracking()
            .Where(g => g.BudgetId == budgetId)
            .AnyAsync(g => g.Name == goalName);
    }

    public async Task<bool> UniqueAccountName(string accountName)
    {
        var budgetId = await _budgetAccessor.GetBudgetId();

        return !await _context.Accounts
            .AsNoTracking()
            .Where(a => a.BudgetId == budgetId)
            .AnyAsync(a => a.Name == accountName);
    }
}