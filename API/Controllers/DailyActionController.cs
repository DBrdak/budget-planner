using Application.DTO;
using Application.DailyActions.DailyExpenditures;
using Application.DailyActions.DailyIncomes;
using Application.DailyActions.DailySavings;
using Microsoft.AspNetCore.Mvc;
using Domain;

namespace API.Controllers
{
    public class DailyActionController : BaseController
    {
        [HttpPost("savings")]
        public async Task<IActionResult> CreateSaving(SavingDto newSaving)
        {
            return HandleResult(await Mediator.Send(new Application.DailyActions.DailySavings.Create.Command() { NewSaving = newSaving }));
        }

        [HttpPost("incomes")]
        public async Task<IActionResult> CreateIncome(IncomeDto newIncome)
        {
            return HandleResult(await Mediator.Send(new Application.DailyActions.DailyIncomes.Create.Command() { NewIncome = newIncome }));
        }

        [HttpDelete("savings/{SavingId}")]
        public async Task<IActionResult> DeleteSaving(Guid savingId)
        {
            return HandleResult(await Mediator.Send(new Application.DailyActions.DailySavings.Delete.Command() { SavingId = savingId }));
        }

        [HttpDelete("incomes/{IncomesId}")]
        public async Task<IActionResult> DeleteIncome(Guid incomeId)
        {
            return HandleResult(await Mediator.Send(new Application.DailyActions.DailyIncomes.Delete.Command() { IncomeId = incomeId }));
        }
    }
}
