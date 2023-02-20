using Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Validation
{
    public class UniqueUser : IUniqueUser
    {
        private readonly DataContext _context;
        private readonly IUserAccessor _userAccessor;
        private readonly IBudgetAccessor _budgetAccessor;

        public UniqueUser(DataContext context, IUserAccessor userAccessor, IBudgetAccessor budgetAccessor)
        {
            _context = context;
            _userAccessor = userAccessor;
            _budgetAccessor = budgetAccessor;
        }

        async Task<bool> IUniqueUser.UniqueBudgetName(string newBudgetName)
        {
            return await _context.Budgets
                .Where(b => b.Name != _budgetAccessor.GetBudgetName())
                .AnyAsync(b => b.Name == newBudgetName);
        }

        async Task<bool> IUniqueUser.UniqueEmail(string newEmail)
        {
            return await _context.Users
                .Where(u => u.UserName != _userAccessor.GetUsername())
                .AnyAsync(u => u.Email == newEmail);
        }

        async Task<bool> IUniqueUser.UniqueUsername(string newUsername)
        {
            return await _context.Users
                .Where(u => u.UserName != _userAccessor.GetUsername() || u.UserName != null)
                .AnyAsync(u => u.UserName == newUsername);
        }
    }
}