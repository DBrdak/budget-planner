using System.ComponentModel;
using Application.Extras.Categories;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("[controller]")]
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