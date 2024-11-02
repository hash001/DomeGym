﻿using ErrorOr;
using GymManagement.Application.Common.Interfaces;
using GymManagement.Domain.Rooms;
using MediatR;

namespace GymManagement.Application.Rooms.Commands.CreateRoom;

public class CreateRoomCommandHandler : IRequestHandler<CreateRoomCommand, ErrorOr<Room>>
{
    private readonly ISubscriptionsRepository _subscriptionsRepository;
    private readonly IGymsRepository _gymsRepository;
    private readonly IUnitOfWork _unitOfWork;


    public CreateRoomCommandHandler(ISubscriptionsRepository subscriptionsRepository, IGymsRepository gymsRepository, IUnitOfWork unitOfWork)
    {
        _subscriptionsRepository = subscriptionsRepository;
        _gymsRepository = gymsRepository;
        _unitOfWork = unitOfWork;

    }
    
    public async Task<ErrorOr<Room>> Handle(CreateRoomCommand request, CancellationToken cancellationToken)
    {
        var gym = await _gymsRepository.GetByIdAsync(request.GymId);

        if (gym is null)
        {
            return Error.NotFound(description: "Gym not found");
        }

        var subscription = await _subscriptionsRepository.GetByIdAsync(gym.SubscriptionId);

        if (subscription is null)
        {
            return Error.NotFound(description: "Subscription not found");
        }

        var room = new Room(request.RoomName, gym.Id, subscription.GetMaxDailySessions());

        var addRoomResult = gym.AddRoom(room);

        if (addRoomResult.IsError)
        {
            return addRoomResult.Errors;
        }

        _gymsRepository.UpdateGym(gym);

        await _unitOfWork.CommitChangesAsync();

        return room;
    }
}