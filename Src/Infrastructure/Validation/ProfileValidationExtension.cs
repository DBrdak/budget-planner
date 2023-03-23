using Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Infrastructure.Validation;

/// <summary>
/// Set of methods to help validators to validate ProfileDto
/// </summary>

public class ProfileValidationExtension : IProfileValidationExtension
{
    private readonly IBudgetAccessor _budgetAccessor;
    private readonly DataContext _context;
    private readonly IUserAccessor _userAccessor;

    public ProfileValidationExtension(DataContext context, IUserAccessor userAccessor, IBudgetAccessor budgetAccessor)
    {
        _context = context;
        _userAccessor = userAccessor;
        _budgetAccessor = budgetAccessor;
    }

    public async Task<bool> UniqueBudgetName(string newBudgetName)
    {
        var budgetName = await _budgetAccessor.GetBudgetName().ConfigureAwait(false);

        return !await _context.Budgets
            .AsNoTracking()
            .Where(b => b.Name != budgetName)
            .AnyAsync(b => b.Name == newBudgetName).ConfigureAwait(false);
    }

    public async Task<bool> UniqueEmail(string newEmail)
    {
        return !await _context.Users
            .AsNoTracking()
            .Where(u => u.UserName != _userAccessor.GetUsername())
            .AnyAsync(u => u.Email == newEmail).ConfigureAwait(false);
    }

    public async Task<bool> UniqueUsername(string newUsername)
    {
        return !await _context.Users
            .AsNoTracking()
            .Where(u => u.UserName != _userAccessor.GetUsername())
            .AnyAsync(u => u.UserName == newUsername).ConfigureAwait(false);
    }
}