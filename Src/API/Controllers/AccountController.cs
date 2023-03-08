using Application.Accounts;
using Application.DTO;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace API.Controllers
{
    public class AccountController : BaseController
    {
        [HttpGet]
        [Description("Gets all accounts from current budget")]
        public async Task<IActionResult> GetAccounts()
        {
            return HandleResult(await Mediator.Send(new List.Query()));
        }

        [HttpDelete("{accountId}")]
        [Description("Delete an account")]
        public async Task<IActionResult> DeleteAccount(Guid accountId)
        {
            return HandleResult(await Mediator.Send(new Delete.Command() { AccountId = accountId }));
        }

        [HttpPut("{accountId}")]
        [Description("Update an account")]
        public async Task<IActionResult> EditAccount(AccountDto newAccount)
        {
            return HandleResult(await Mediator.Send(new Edit.Command() { NewAccount = newAccount }));
        }

        [HttpPost]
        [Description("Create new account")]
        public async Task<IActionResult> CreateAccount(AccountDto newAccount)
        {
            return HandleResult(await Mediator.Send(new Create.Command() { NewAccount = newAccount }));
        }
    }
}