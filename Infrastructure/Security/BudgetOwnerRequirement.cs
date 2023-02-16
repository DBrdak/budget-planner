using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Security
{
    public class BudgetOwnerRequirement : IAuthorizationRequirement
    {
    }

    public class BudgetOwnerRequirementHandler : AuthorizationHandler<BudgetOwnerRequirement>
    {
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public BudgetOwnerRequirementHandler(DataContext dbContext,
            IHttpContextAccessor httpContextAccessor)
        {
            _context = dbContext;
            _httpContextAccessor = httpContextAccessor;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, BudgetOwnerRequirement requirement)
        {
            var userId = context.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
                return Task.CompletedTask;

            var budgetName = _httpContextAccessor.HttpContext.Request.RouteValues
                .SingleOrDefault(x => x.Key == "budgetName").Value.ToString();

            var user = _context.Budgets
                .AsNoTracking()
                .Include(x => x.User)
                .SingleOrDefaultAsync(x => x.User.Id == userId && x.Name == budgetName).Result;

            if (user == null)
                return Task.CompletedTask;

            context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}