using System.ComponentModel;
using Application.DTO;
using Application.SpendingPlan.Savings;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

/// <summary>
/// Set of HTTP requests for managing spending plan in current budget
/// </summary>

[Route("[controller]")]
public class SpendingPlanController : BaseController
{
    [HttpGet("savings")]
    [Description("Gets all planned savings")]
    public async Task<IActionResult> GetFutureSavings()
    {
        return HandleResult(await Mediator.Send(new List.Query()));
    }

    [HttpGet("incomes")]
    [Description("Gets all planned incomes")]
    public async Task<IActionResult> GetFutureIncomes()
    {
        return HandleResult(await Mediator.Send(new Application.SpendingPlan.Incomes.List.Query()));
    }

    [HttpGet("expenditures")]
    [Description("Gets all planned expenditures")]
    public async Task<IActionResult> GetFutureExpenditures()
    {
        return HandleResult(await Mediator.Send(new Application.SpendingPlan.Expenditures.List.Query()));
    }

    [HttpPost("savings")]
    [Description("Adds a new saving to spending plan")]
    public async Task<IActionResult> CreateFutureSaving(FutureSavingDto newFutureSaving)
    {
        return HandleResult(await Mediator.Send(new Create.Command { NewFutureSaving = newFutureSaving }));
    }

    [HttpPost("incomes")]
    [Description("Adds a new income to spending plan")]
    public async Task<IActionResult> CreateFutureIncome(FutureIncomeDto newFutureIncome)
    {
        return HandleResult(await Mediator.Send(new Application.SpendingPlan.Incomes.Create.Command
            { NewFutureIncome = newFutureIncome }));
    }

    [HttpPost("expenditures")]
    [Description("Adds a new expenditure to spending plan")]
    public async Task<IActionResult> CreateFutureExpenditure(FutureExpenditureDto newFutureExpenditure)
    {
        return HandleResult(await Mediator.Send(new Application.SpendingPlan.Expenditures.Create.Command
            { NewFutureExpenditure = newFutureExpenditure }));
    }

    [HttpDelete("savings/{futureSavingId}")]
    [Description("Deletes a saving from spending plan")]
    public async Task<IActionResult> DeleteFutureSaving(Guid futureSavingId)
    {
        return HandleResult(await Mediator.Send(new Delete.Command { FutureSavingId = futureSavingId }));
    }

    [HttpDelete("incomes/{futureIncomeId}")]
    [Description("Deletes a income from spending plan")]
    public async Task<IActionResult> DeleteFutureIncome(Guid futureIncomeId)
    {
        return HandleResult(await Mediator.Send(new Application.SpendingPlan.Incomes.Delete.Command
            { FutureIncomeId = futureIncomeId }));
    }

    [HttpDelete("expenditures/{futureExpenditureId}")]
    [Description("Deletes a expenditure from spending plan")]
    public async Task<IActionResult> DeleteFutureExpenditure(Guid futureExpenditureId)
    {
        return HandleResult(await Mediator.Send(new Application.SpendingPlan.Expenditures.Delete.Command
            { FutureExpenditureId = futureExpenditureId }));
    }
}