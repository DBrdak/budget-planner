using Application.SpendingPlan.Incomes;
using Application.SpendingPlan.Savings;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class SpendingPlanController : BaseController
    {
        [HttpGet("savings")]
        public async Task<IActionResult> GetFutureSavings()
        {
            return HandleResult(await Mediator.Send(new Application.SpendingPlan.Savings.List.Query()));
        }

        [HttpGet("incomes")]
        public async Task<IActionResult> GetFutureTransactions()
        {
            return HandleResult(await Mediator.Send(new Application.SpendingPlan.Incomes.List.Query()));
        }
    }
}