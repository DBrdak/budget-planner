using Application.Core;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

/// <summary>
/// Base class for controllers in Budget-Planner API
/// </summary>

[ApiController]
[Route("api/{budgetName}/[controller]")]
[Authorize(Policy = "IsBudgetOwner")]
public class BaseController : ControllerBase
{
    private IMediator _mediator;

    protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

    protected IActionResult HandleResult<T>(Result<T> result)
    {
        if (result == null)
            return NotFound();

        if (result.IsSuccess)
            return Ok(result.Value);

        return BadRequest(result.Error);
    }
}