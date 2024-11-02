using ErrorOr;
using GymManagement.Application.Common.Interfaces;
using MediatR;

namespace GymManagement.Application.Gyms.Commands.CreateGym;

public class CreateGymCommandHandler : IRequestHandler<CreateGymCommand, ErrorOr<Domain.Gyms.Gym>>
{
    private readonly IGymsRepository _gymsRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISubscriptionsRepository _subscriptionsRepository;

    public CreateGymCommandHandler(IGymsRepository gymsRepository, IUnitOfWork unitOfWork,
        ISubscriptionsRepository subscriptionsRepository)
    {
        _gymsRepository = gymsRepository;
        _unitOfWork = unitOfWork;
        _subscriptionsRepository = subscriptionsRepository;
    }

    public async Task<ErrorOr<Domain.Gyms.Gym>> Handle(CreateGymCommand request, CancellationToken cancellationToken)
    {
        var subscription = await _subscriptionsRepository.GetByIdAsync(request.SubscriptionId);
        if (subscription is null)
        {
            return Error.NotFound(description: "Subscription not found");
        }
        
        var gym = new Domain.Gyms.Gym(request.Name, subscription.GetMaxRooms(), subscription.Id);

        var addGymResult = subscription.AddGym(gym);

        if (addGymResult.IsError)
        {
            return addGymResult.Errors;
        }

        _subscriptionsRepository.Update(subscription);
        
        await _gymsRepository.AddGymAsync(gym);

        await _unitOfWork.CommitChangesAsync();

        return gym;
    }
}