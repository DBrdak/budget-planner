using System.ComponentModel;
using Application.DailyActions.DailySavings;
using Application.DTO;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

/// <summary>
/// Set of HTTP requests for adding and deleting specific entities in budget
/// </summary>

public class BudgetController : BaseController
{
    [HttpPost("savings")]
    [Description("Adds new saving in real time")]
    public async Task<IActionResult> CreateSaving(SavingDto newSaving)
    {
        return HandleResult(await Mediator.Send(new Create.Command { NewSaving = newSaving }));
    }

    [HttpPost("incomes")]
    [Description("Adds new income in real time")]
    public async Task<IActionResult> CreateIncome(IncomeDto newIncome)
    {
        return HandleResult(await Mediator.Send(new Application.DailyActions.DailyIncomes.Create.Command
            { NewIncome = newIncome }));
    }

    [HttpPost("expenditures")]
    [Description("Adds new expenditure in real time")]
    public async Task<IActionResult> CreateExpenditure(ExpenditureDto newExpenditure)
    {
        return HandleResult(await Mediator.Send(new Application.DailyActions.DailyExpenditures.Create.Command
            { NewExpenditure = newExpenditure }));
    }
    
    [HttpDelete("savings/{savingId}")]
    [Description("Deletes saving from the past")]
    public async Task<IActionResult> DeleteSaving(Guid savingId)
    {
        return HandleResult(await Mediator.Send(new Delete.Command { SavingId = savingId }));
    }

    [HttpDelete("incomes/{incomeId}")]
    [Description("Deletes income from the past")]
    public async Task<IActionResult> DeleteIncome(Guid incomeId)
    {
        return HandleResult(await Mediator.Send(new Application.DailyActions.DailyIncomes.Delete.Command
            { IncomeId = incomeId }));
    }

    [HttpDelete("expenditures/{expenditureId}")]
    [Description("Deletes expenditure from the past")]
    public async Task<IActionResult> DeleteExpenditure(Guid expenditureId)
    {
        return HandleResult(await Mediator.Send(new Application.DailyActions.DailyExpenditures.Delete.Command
            { ExpenditureId = expenditureId }));
    }
}