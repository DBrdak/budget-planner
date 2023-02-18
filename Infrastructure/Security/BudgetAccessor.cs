using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Security
{
    public class BudgetAccessor : IBudgetAccessor
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public BudgetAccessor(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetBudgetName()
        {
            return _httpContextAccessor.HttpContext.Request.RouteValues
                .SingleOrDefault(x => x.Key == "budgetName").Value.ToString();
        }
    }
}