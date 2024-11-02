using ErrorOr;
using GymManagement.Application.Common.Interfaces;
using GymManagement.Domain.Gyms;
using MediatR;

namespace GymManagement.Application.Gyms.Queries.ListGyms;

public class ListGymsQueryHandler : IRequestHandler<ListGymsQuery, ErrorOr<List<Gym>>>
{
    private readonly ISubscriptionsRepository _subscriptionsRepository;
    private readonly IGymsRepository _gymsRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ListGymsQueryHandler(ISubscriptionsRepository subscriptionsRepository, IGymsRepository gymsRepository, IUnitOfWork unitOfWork)
    {
        _subscriptionsRepository = subscriptionsRepository;
        _gymsRepository = gymsRepository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<ErrorOr<List<Gym>>> Handle(ListGymsQuery request, CancellationToken cancellationToken)
    {
        var subscriptionExists = await _subscriptionsRepository.ExistsAsync(request.SubscriptionId);

        if (!subscriptionExists)
        {
            return Error.NotFound(description: "Subscription not found");
        }

        var gyms = await _gymsRepository.ListBySubscriptionIdAsync(request.SubscriptionId);

        return gyms;
    }
}