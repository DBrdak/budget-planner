using System.ComponentModel;
using Application.Extras.Accounts;
using Application.Extras.Categories;
using Application.Extras.Goals;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

/// <summary>
/// Set of HTTP requests for getting categories in current budget
/// </summary>

public class ExtrasController : BaseController
{
    [HttpGet("categories/expenditure")]
    [Description("Gets list of expenditure categories")]
    public async Task<IActionResult> GetExpenditureCategories()
    {
        return HandleResult(await Mediator.Send(new ExpendituresList.Query()));
    }

    [HttpGet("categories/income")]
    [Description("Gets list of income categories")]
    public async Task<IActionResult> GetIncomeCategories()
    {
        return HandleResult(await Mediator.Send(new IncomesList.Query()));
    }

    [HttpGet("accounts/checking")]
    public async Task<IActionResult> GetCheckingAccountsNames()
    {
        return HandleResult(await Mediator.Send(new CheckingAccounts.Query()));
    }

    [HttpGet("accounts/saving")]
    public async Task<IActionResult> GetSavingAccountsNames()
    {
        return HandleResult(await Mediator.Send(new SavingAccounts.Query()));
    }

    [HttpGet("goals")]
    public async Task<IActionResult> GetGoalNames()
    {
        return HandleResult(await Mediator.Send(new GoalsName.Query()));
    }
}