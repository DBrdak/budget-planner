using Application.Extras.Categories;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Controllers
{
    public class ExtrasController : BaseController
    {
        [HttpGet("expenditures/categories")]
        public async Task<IActionResult> GetExpenditureCategories()
        {
            return HandleResult(await Mediator.Send(new ExpendituresList.Query()));
        }

        [HttpGet("incomes/categories")]
        public async Task<IActionResult> GetIncomeCategories()
        {
            return HandleResult(await Mediator.Send(new IncomesList.Query()));
        }
    }
}