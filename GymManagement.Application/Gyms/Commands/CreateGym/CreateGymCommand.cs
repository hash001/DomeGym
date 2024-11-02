using ErrorOr;
using MediatR;

namespace GymManagement.Application.Gyms.Commands.CreateGym;

public record CreateGymCommand(string Name, Guid SubscriptionId) : IRequest<ErrorOr<Domain.Gyms.Gym>>;