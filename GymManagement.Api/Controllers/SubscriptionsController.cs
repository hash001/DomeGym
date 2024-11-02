using GymManagement.Application.Subscriptions.Commands.CreateSubscription;
using GymManagement.Application.Subscriptions.Queries;
using Microsoft.AspNetCore.Mvc;
using GymManagement.Contracts.Subscriptions;
using MediatR;
using SubscriptionType = GymManagement.Domain.Subscriptions.SubscriptionType;

namespace GymManagement.Api.Controllers;

[Route("[controller]")]
public class SubscriptionsController : ApiController
{
    private readonly ISender _mediator;

    public SubscriptionsController(ISender mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateSubscription(CreateSubscriptionRequest request)
    {
        if (!Enum.TryParse<SubscriptionType>(request.SubscriptionTypeV1.ToString(), out var subscriptionType))
        {
            return Problem(statusCode: StatusCodes.Status400BadRequest, detail: "Invalid subscription type");
        }

        var command = new CreateSubscriptionCommand(subscriptionType, request.AdminId);

        var result = await _mediator.Send(command);

        // return result.MatchFirst(
        //     guid => Ok(new SubscriptionResponse(guid, request.SubscriptionType)),
        //     error => Problem());

        return result.Match(
            subscription => Ok(new SubscriptionResponse(subscription.Id, request.SubscriptionTypeV1)),
            errors => Problem(string.Join(Environment.NewLine, errors)));

        // if (result.IsError)
        // {
        //     return Problem();
        // }
        //
        // var response = new SubscriptionResponse(
        //     result.Value,
        //     request.SubscriptionType);
        //
        // return Ok(response);
    }

    [HttpGet("{subscriptionId:guid}")]
    public async Task<IActionResult> GetSubscription(Guid subscriptionId)
    {
        var query = new GetSubscriptionQuery(subscriptionId);

        var getSubscriptionsResult = await _mediator.Send(query);

        return getSubscriptionsResult.MatchFirst(
            subscription => Ok(new SubscriptionResponse(
                subscription.Id,
                Enum.Parse<SubscriptionTypeV1>(subscription.SubscriptionType.ToString()))),
            _ => Problem());
    }
}