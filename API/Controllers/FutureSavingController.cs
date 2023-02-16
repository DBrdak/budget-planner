using Application.FutureSavings;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class FutureSavingController : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetFutureSavings()
        {
            return HandleResult(await Mediator.Send(new List.Query()));
        }
    }
}
