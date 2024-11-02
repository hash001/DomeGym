using ErrorOr;
using MediatR;

namespace GymManagement.Application.Gyms.Queries.GetGym;

public record GetGymQuery(Guid SubscriptionId, Guid GymId) : IRequest<ErrorOr<Domain.Gyms.Gym>>;