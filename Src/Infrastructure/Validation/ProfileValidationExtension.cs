using Application.Interfaces;
using Infrastructure.Security;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Infrastructure.Validation
{
    public class ProfileValidationExtension : IProfileValidationExtension
    {
        private readonly DataContext _context;
        private readonly IUserAccessor _userAccessor;
        private readonly IBudgetAccessor _budgetAccessor;

        public ProfileValidationExtension(DataContext context, IUserAccessor userAccessor, IBudgetAccessor budgetAccessor)
        {
            _context = context;
            _userAccessor = userAccessor;
            _budgetAccessor = budgetAccessor;
        }

        public async Task<bool> UniqueBudgetName(string newBudgetName)
        {
            var budgetName = await _budgetAccessor.GetBudgetName();

            return await _context.Budgets
                .AsNoTracking()
                .Where(b => b.Name != budgetName)
                .AnyAsync(b => b.Name == newBudgetName);
        }

        public async Task<bool> UniqueEmail(string newEmail)
        {
            return await _context.Users
            .AsNoTracking()
                .Where(u => u.UserName != _userAccessor.GetUsername())
                .AnyAsync(u => u.Email == newEmail);
        }

        public async Task<bool> UniqueUsername(string newUsername)
        {
            return await _context.Users
            .AsNoTracking()
                .Where(u => u.UserName != _userAccessor.GetUsername())
                .AnyAsync(u => u.UserName == newUsername);
        }
    }
}