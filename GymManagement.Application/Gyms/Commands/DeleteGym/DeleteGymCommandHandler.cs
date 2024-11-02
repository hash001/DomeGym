using ErrorOr;
using GymManagement.Application.Common.Interfaces;
using MediatR;

namespace GymManagement.Application.Gyms.Commands.DeleteGym;

public class DeleteGymCommandHandler : IRequestHandler<DeleteGymCommand, ErrorOr<Deleted>>
{
    private readonly ISubscriptionsRepository _subscriptionsRepository;
    private readonly IGymsRepository _gymsRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteGymCommandHandler(ISubscriptionsRepository subscriptionsRepository, IGymsRepository gymsRepository,
        IUnitOfWork unitOfWork)
    {
        _subscriptionsRepository = subscriptionsRepository;
        _gymsRepository = gymsRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<Deleted>> Handle(DeleteGymCommand request, CancellationToken cancellationToken)
    {
        var gym = await _gymsRepository.GetByIdAsync(request.GymId);

        if (gym is null)
        {
            return Error.NotFound(description: "Gym not found");
        }
        
        var subscription = await _subscriptionsRepository.GetByIdAsync(request.SubscriptionId);

        if (subscription is null)
        {
            return Error.NotFound(description: "Subscription not found");
        }

        var hasGym = subscription.HasGym(request.GymId);

        if (!hasGym)
        {
            return Error.Unexpected(description: "Gym does not exist for subscription");
        }
        
        subscription.RemoveGym(request.GymId);
        _subscriptionsRepository.Update(subscription);
        _gymsRepository.RemoveGym(gym);
        await _unitOfWork.CommitChangesAsync();

        return Result.Deleted;
    }
}