using Application.DTO;
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

        [HttpPut("{goalId}")]
        public async Task<IActionResult> EditAccount(Guid goalId, GoalDto newGoal)
        {
            return HandleResult(await Mediator.Send(new Edit.Command() { GoalId = goalId, NewGoal = newGoal }));
        }

        [HttpPost]
        public async Task<IActionResult> CreateGoal(GoalDto newGoal)
        {
            return HandleResult(await Mediator.Send(new Create.Command() { NewGoal = newGoal }));
        }
    }
}