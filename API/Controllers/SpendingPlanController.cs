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

        [HttpPost("savings")]
        public async Task<IActionResult> CreateFutureSaving(FutureSavingDto newFutureSaving)
        {
            return HandleResult(await Mediator.Send(new Application.SpendingPlan.Savings.Create.Command() { NewFutureSaving = newFutureSaving }));
        }

        [HttpPost("incomes")]
        public async Task<IActionResult> CreateFutureIncome(FutureIncomeDto newFutureIncome)
        {
            return HandleResult(await Mediator.Send(new Application.SpendingPlan.Incomes.Create.Command() { NewFutureIncome = newFutureIncome }));
        }

        [HttpPost("expenditures")]
        public async Task<IActionResult> CreateFutureExpenditure(FutureExpenditureDto newFutureExpenditure)
        {
            return HandleResult(await Mediator.Send(new Application.SpendingPlan.Expenditures.Create.Command() { NewFutureExpenditure = newFutureExpenditure }));
        }

        [HttpDelete("savings/{futureSavingId}")]
        public async Task<IActionResult> DeleteFutureSaving(Guid futureSavingId)
        {
            return HandleResult(await Mediator.Send(new Application.SpendingPlan.Savings.Delete.Command() { FutureSavingId = futureSavingId }));
        }

        [HttpDelete("incomes/{futureIncomeId}")]
        public async Task<IActionResult> DeleteFutureIncome(Guid futureIncomeId)
        {
            return HandleResult(await Mediator.Send(new Application.SpendingPlan.Incomes.Delete.Command() { FutureIncomeId = futureIncomeId }));
        }

        [HttpDelete("expenditures/{futureExpenditureId}")]
        public async Task<IActionResult> DeleteFutureExpenditure(Guid futureExpenditureId)
        {
            return HandleResult(await Mediator.Send(new Application.SpendingPlan.Expenditures.Delete.Command() { FutureExpenditureId = futureExpenditureId }));
        }
    }
}