namespace GymManagement.Application.Common.Interfaces;

using Gym = Domain.Gyms.Gym;

public interface IGymsRepository
{
    Task AddGymAsync(Gym gym);
    Task<Gym?> GetByIdAsync(Guid id);
    Task<bool> ExistsAsync(Guid id);
    Task<List<Gym>> ListBySubscriptionIdAsync(Guid subscriptionId);
    void UpdateGym(Gym gym);
    void RemoveGym(Gym gym);
    void RemoveRange(List<Gym> gyms);
}