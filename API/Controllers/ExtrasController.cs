using Application.Extras.Categories;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Controllers
{
    public class ExtrasController : BaseController
    {
        [HttpGet("expenditures/categories")]
        [Description("Gets list of expenditure categories")]
        public async Task<IActionResult> GetExpenditureCategories()
        {
            return HandleResult(await Mediator.Send(new ExpendituresList.Query()));
        }

        [HttpGet("incomes/categories")]
        [Description("Gets list of income categories")]
        public async Task<IActionResult> GetIncomeCategories()
        {
            return HandleResult(await Mediator.Send(new IncomesList.Query()));
        }
    }
}