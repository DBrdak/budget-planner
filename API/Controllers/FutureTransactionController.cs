using Application.FutureTransactions;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class FutureTransactionController : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetFutureTransactions()
        {
            return HandleResult(await Mediator.Send(new List.Query()));
        }
    }
}
