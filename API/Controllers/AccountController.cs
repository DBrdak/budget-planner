using Application.Accounts;
using Application.DTO;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class AccountController : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetAccounts()
        {
            return HandleResult(await Mediator.Send(new List.Query()));
        }

        [HttpDelete("{accountId}")]
        public async Task<IActionResult> DeleteAccount(Guid accountId)
        {
            return HandleResult(await Mediator.Send(new Delete.Command() { AccountId = accountId }));
        }

        [HttpPut("{accountId}")]
        public async Task<IActionResult> EditAccount(Guid accountId, AccountDto newAccount)
        {
            return HandleResult(await Mediator.Send(new Edit.Command() { AccountId = accountId, NewAccount = newAccount }));
        }

        [HttpPost]
        public async Task<IActionResult> CreateAccount(AccountDto newAccount)
        {
            return HandleResult(await Mediator.Send(new Create.Command() { NewAccount = newAccount }));
        }
    }
}