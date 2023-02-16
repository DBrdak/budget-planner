using Application.Goals;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class GoalsController : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetGoals()
        {
            return HandleResult(await Mediator.Send(new List.Query()));
        }

        [HttpDelete("{goalId}")]
        public async Task<IActionResult> DeleteGoal(Guid goalId)
        {
            return HandleResult(await Mediator.Send(new Delete.Command() { GoalId = goalId }));
        }
    }
}