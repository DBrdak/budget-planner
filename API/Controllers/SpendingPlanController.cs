using Application.DTO;
using Application.SpendingPlan.Expenditures;
using Application.SpendingPlan.Incomes;
using Application.SpendingPlan.Savings;
using Domain;
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

        [HttpGet("expenditures")]
        public async Task<IActionResult> GetFutureExpenditures()
        {
            return HandleResult(await Mediator.Send(new Application.SpendingPlan.Expenditures.List.Query()));
        }

        [HttpPost]
        public async Task<IActionResult> CreateFutureSaving(FutureSavingDto newFutureExpenditure)
        {
            return HandleResult(await Mediator.Send(new Application.SpendingPlan.Expenditures.Create.Command() { NewFutureExpenditure = newFutureExpenditure }));
        }

        [HttpPost]
        public async Task<IActionResult> CreateFutureIncome(FutureIncomeDto newFutureIncome)
        {
            return HandleResult(await Mediator.Send(new Application.SpendingPlan.Incomes.Create.Command() { NewFutureIncome = newFutureIncome }));
        }

        [HttpPost]
        public async Task<IActionResult> CreateFutureExpenditure(FutureExpenditureDto newFutureExpenditure)
        {
            return HandleResult(await Mediator.Send(new Application.SpendingPlan.Expenditures.Create.Command() { NewFutureExpenditure = newFutureExpenditure}));
        }

        [HttpDelete("{budgetId}")]
        public async Task<IActionResult> DeleteFutureSaving(Guid FutureExpenditureId)
        {
            return HandleResult(await Mediator.Send(new Delete.Command() { FutureExpenditureId = FutureExpenditureId }));
        }
    }
}