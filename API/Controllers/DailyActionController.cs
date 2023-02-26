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
        [HttpDelete("savings/{SavingId}")]
        public async Task<IActionResult> DeleteSaving(Guid savingId)
        {
            return HandleResult(await Mediator.Send(new Application.DailyActions.DailySavings.Delete.Command() { SavingId = savingId }));
        }
    }
}
