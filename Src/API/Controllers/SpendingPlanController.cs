﻿using System.ComponentModel;
using Application.DTO;
using Application.SpendingPlan.Savings;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

/// <summary>
/// Set of HTTP requests for managing spending plan in current budget
/// </summary>

public class SpendingPlanController : BaseController
{
    [HttpGet("savings/{date}")]
    [Description("Gets all planned savings")]
    public async Task<IActionResult> GetFutureSavings(DateTime date)
    {
        return HandleResult(await Mediator.Send(new List.Query { Date = date }));
    }

    [HttpGet("incomes/{date}")]
    [Description("Gets all planned incomes")]
    public async Task<IActionResult> GetFutureIncomes(DateTime date)
    {
        return HandleResult(await Mediator.Send(new Application.SpendingPlan.Incomes.List.Query { Date = date }));
    }

    [HttpGet("expenditures/{date}")]
    [Description("Gets all planned expenditures")]
    public async Task<IActionResult> GetFutureExpenditures(DateTime date)
    {
        return HandleResult(await Mediator.Send(new Application.SpendingPlan.Expenditures.List.Query { Date = date }));
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