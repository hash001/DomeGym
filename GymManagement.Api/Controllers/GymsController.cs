﻿using GymManagement.Application.Gyms.Commands.AddTrainer;
using GymManagement.Application.Gyms.Commands.CreateGym;
using GymManagement.Application.Gyms.Commands.DeleteGym;
using GymManagement.Application.Gyms.Queries.GetGym;
using GymManagement.Application.Gyms.Queries.ListGyms;
using GymManagement.Contracts.Gyms;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GymManagement.Api.Controllers;

[Route("subscriptions/{subscriptionId:guid}/gyms")]
public class GymsController : ApiController
{
    private readonly ISender _mediator;

    public GymsController(ISender mediator)
    {
        _mediator = mediator;
    }

    [HttpPost()]
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

    [HttpDelete("{gymId:guid}")]
    public async Task<IActionResult> DeleteGym(Guid subscriptionId, Guid gymId)
    {
        var command = new DeleteGymCommand(subscriptionId, gymId);

        var deleteGymResult = await _mediator.Send(command);

        return deleteGymResult.Match(
            _ => NoContent(),
            Problem);
    }

    [HttpGet]
    public async Task<IActionResult> ListGyms(Guid subscriptionId)
    {
        var command = new ListGymsQuery(subscriptionId);

        var listGymsResult = await _mediator.Send(command);

        return listGymsResult.Match(
            gyms => Ok(gyms.ConvertAll(gym => new GymResponse(gym.Id, gym.Name))),
            Problem);
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
    
    [HttpPost("{gymId:guid}/trainers")]
    public async Task<IActionResult> AddTrainer(AddTrainerRequest request, Guid subscriptionId, Guid gymId)
    {
        var command = new AddTrainerCommand(subscriptionId, gymId, request.TrainerId);

        var addTrainerResult = await _mediator.Send(command);

        return addTrainerResult.Match(
            success => Ok(),
            Problem);
    }
}