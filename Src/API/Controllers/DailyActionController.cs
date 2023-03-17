using Application.DTO;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace API.Controllers
{
    public class DailyActionController : BaseController
    {
        [HttpPost("savings")]
        [Description("Adds new saving in real time")]
        public async Task<IActionResult> CreateSaving(SavingDto newSaving)
        {
            return HandleResult(await Mediator.Send(new Application.DailyActions.DailySavings.Create.Command() { NewSaving = newSaving }));
        }

        [HttpPost("incomes")]
        [Description("Adds new income in real time")]
        public async Task<IActionResult> CreateIncome(IncomeDto newIncome)
        {
            return HandleResult(await Mediator.Send(new Application.DailyActions.DailyIncomes.Create.Command() { NewIncome = newIncome }));
        }

        [HttpPost("expenditures")]
        [Description("Adds new expenditure in real time")]
        public async Task<IActionResult> CreateExpenditure(ExpenditureDto newExpenditure)
        {
            return HandleResult(await Mediator.Send(new Application.DailyActions.DailyExpenditures.Create.Command() { NewExpenditure = newExpenditure }));
        }

        // Zróć uwagę na wielkość liter w url i zmiennej w funkcji, dla każdego delete'a
        [HttpDelete("savings/{SavingId}")]
        [Description("Deletes saving from the past")]
        public async Task<IActionResult> DeleteSaving(Guid savingId)
        {
            return HandleResult(await Mediator.Send(new Application.DailyActions.DailySavings.Delete.Command() { SavingId = savingId }));
        }

        [HttpDelete("incomes/{IncomeId}")]
        [Description("Deletes income from the past")]
        public async Task<IActionResult> DeleteIncome(Guid incomeId)
        {
            return HandleResult(await Mediator.Send(new Application.DailyActions.DailyIncomes.Delete.Command() { IncomeId = incomeId }));
        }

        [HttpDelete("expenditures/{ExpenditureId}")]
        [Description("Deletes expenditure from the past")]
        public async Task<IActionResult> DeleteExpenditure(Guid expenditureId)
        {
            return HandleResult(await Mediator.Send(new Application.DailyActions.DailyExpenditures.Delete.Command() { ExpenditureId = expenditureId }));
        }
    }
}