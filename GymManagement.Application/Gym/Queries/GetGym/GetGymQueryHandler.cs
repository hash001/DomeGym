using ErrorOr;
using GymManagement.Application.Common.Interfaces;
using MediatR;

namespace GymManagement.Application.Gym.Queries.GetGym;

public class GetGymQueryHandler : IRequestHandler<GetGymQuery, ErrorOr<Domain.Gyms.Gym>> 
{
    private readonly IGymsRepository _gymsRepository;
    private readonly ISubscriptionsRepository _subscriptionsRepository;

    public GetGymQueryHandler(IGymsRepository gymsRepository, ISubscriptionsRepository subscriptionsRepository)
    {
        _gymsRepository = gymsRepository;
        _subscriptionsRepository = subscriptionsRepository;
    }
    
    public async Task<ErrorOr<Domain.Gyms.Gym>> Handle(GetGymQuery request, CancellationToken cancellationToken)
    {
        if (await _subscriptionsRepository.GetByIdAsync(request.SubscriptionId) == null)
        {
            return Error.NotFound("Subscription not found");
        }
        
        var gym = await _gymsRepository.GetByIdAsync(request.GymId);

        if (gym == null)
        {
            return Error.NotFound("Gym not found");
        }
        
        return gym;
    }
}