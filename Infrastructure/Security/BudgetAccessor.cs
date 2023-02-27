using Application.Interfaces;
using Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Security
{
    public class BudgetAccessor : IBudgetAccessor
    {
        private readonly DataContext _context;
        private readonly IUserAccessor _userAccessor;

        public BudgetAccessor(DataContext context, IUserAccessor userAccessor)
        {
            _context = context;
            _userAccessor = userAccessor;
        }

        public string GetBudgetName()
        {
            return _context.Budgets
                .FirstOrDefault(b => b.User.UserName == _userAccessor.GetUsername()).Name;
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
}