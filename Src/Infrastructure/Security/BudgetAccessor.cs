using Application.Interfaces;
using Domain;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Infrastructure.Security;

/// <summary>
/// Set of methods to retrive various data about current budget form database
/// </summary>

public class BudgetAccessor : IBudgetAccessor
{
    private readonly DataContext _context;
    private readonly IUserAccessor _userAccessor;

    public BudgetAccessor(DataContext context, IUserAccessor userAccessor)
    {
        _context = context;
        _userAccessor = userAccessor;
    }

    public async Task<string> GetBudgetName()
    {
        return (await _context.Budgets
            .FirstOrDefaultAsync(b => b.User.UserName == _userAccessor.GetUsername())
            .ConfigureAwait(false)).Name;
    }

    public async Task<Budget> GetBudget()
    {
        return await _context.Budgets
            .FirstOrDefaultAsync(b => b.User.UserName == _userAccessor.GetUsername());
    }

    public async Task<Guid> GetBudgetId()
    {
        var budget = await _context.Budgets
            .FirstOrDefaultAsync(b => b.User.UserName == _userAccessor.GetUsername());

        return budget.Id;
    }
}