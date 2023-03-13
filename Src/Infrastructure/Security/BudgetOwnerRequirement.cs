using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System.Security.Claims;

namespace Infrastructure.Security
{
    public class BudgetOwnerRequirement : IAuthorizationRequirement
    {
    }

    public class BudgetOwnerRequirementHandler : AuthorizationHandler<BudgetOwnerRequirement>
    {
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IBudgetAccessor _budgetAccessor;

        public BudgetOwnerRequirementHandler(DataContext dbContext,
            IHttpContextAccessor httpContextAccessor, IBudgetAccessor budgetAccessor)
        {
            _context = dbContext;
            _httpContextAccessor = httpContextAccessor;
            _budgetAccessor = budgetAccessor;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, BudgetOwnerRequirement requirement)
        {
            var userId = context.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
                return Task.CompletedTask;

            var budgetName = _budgetAccessor.GetBudgetName().Result;

            var user = _context.Budgets
                .AsNoTracking()
                .Include(x => x.User)
                .SingleOrDefault(x => x.User.Id == userId && x.Name == budgetName);

            if (user == null)
                return Task.CompletedTask;

            context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}