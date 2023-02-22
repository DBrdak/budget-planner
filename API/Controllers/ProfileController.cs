using Application.DTO;
using Application.Profiles;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class ProfileController : BaseController
    {
        [HttpGet("{username}")]
        public async Task<IActionResult> GetProfile()
        {
            return HandleResult(await Mediator.Send(new Details.Query()));
        }

        [HttpDelete("{username}")]
        public async Task<IActionResult> DeleteGoal([FromBody] string password)
        {
            return HandleResult(await Mediator.Send(new Delete.Command() { Password = password }));
        }

        [HttpPut("{username}")]
        public async Task<IActionResult> EditAccount(ProfileDto newProfile)
        {
            return HandleResult(await Mediator.Send(new Edit.Command() { NewProfile = newProfile }));
        }

        [HttpPut("{username}/password")]
        public async Task<IActionResult> UpdatePassword(PasswordFormDto passwordForm)
        {
            return HandleResult(await Mediator.Send(new UpdatePassword.Command() { PasswordForm = passwordForm }));
        }
    }
}