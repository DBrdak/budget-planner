using System.ComponentModel;
using Application.DTO;
using Application.Profiles;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

/// <summary>
/// Set of HTTP requests for managing profile of current user
/// </summary>

public class ProfileController : BaseController
{
    [HttpGet("{username}")]
    [Description("Gets user profile")]
    public async Task<IActionResult> GetProfile()
    {
        return HandleResult(await Mediator.Send(new Details.Query()));
    }

    [HttpPut]
    [Description("Deletes entire user data")]
    public async Task<IActionResult> DeleteUser([FromBody] string password)
    {
        return HandleResult(await Mediator.Send(new Delete.Command { Password = password }));
    }

    [HttpPut("{username}")]
    [Description("Updates profile")]
    public async Task<IActionResult> EditProfile(ProfileDto newProfile)
    {
        return HandleResult(await Mediator.Send(new Edit.Command { NewProfile = newProfile }));
    }

    [HttpPut("{username}/password")]
    [Description("Updates a password")]
    public async Task<IActionResult> UpdatePassword(PasswordFormDto passwordForm)
    {
        return HandleResult(await Mediator.Send(new UpdatePassword.Command { PasswordForm = passwordForm }));
    }
}