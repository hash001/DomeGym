using GymManagement.Application.Gym.Commands.CreateGym;
using GymManagement.Application.Gym.Queries.GetGym;
using GymManagement.Contracts.Gyms;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GymManagement.Api.Controllers;

[Route("[controller]")]
public class GymController : ApiController
{
    private readonly ISender _mediator;

    public GymController(ISender mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateGym(CreateGymRequest createGymRequest, Guid subscriptionId)
    {
        if (string.IsNullOrWhiteSpace(createGymRequest.Name))
        {
            return BadRequest("Invalid Gym name");
        }

        var command = new CreateGymCommand(createGymRequest.Name, subscriptionId);

        var result = await _mediator.Send(command);

        return result.Match(
            gym => CreatedAtAction(
                nameof(GetGym),
                new { subscriptionId, GymId = gym.Id },
                new GymResponse(gym.Id, gym.Name)),
            errors => Problem(errors.FirstOrDefault()));
    }

    [HttpGet("{gymId:guid}")]
    public async Task<IActionResult> GetGym(Guid subscriptionId, Guid gymId)
    {
        var query = new GetGymQuery(subscriptionId, gymId);
        var getGymResult = await _mediator.Send(query);

        return getGymResult.Match(
            gym => Ok(new GymResponse(gym.Id, gym.Name)),
            errors => Problem(errors.FirstOrDefault()));
    }
}