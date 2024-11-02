using GymManagement.Domain.Subscriptions;

namespace GymManagement.Application.Common.Interfaces;

public interface ISubscriptionsRepository
{
    Task AddAsync(Subscription subscription);
    
    Task<bool> ExistsAsync(Guid id);
    
    Task<Subscription?> GetByIdAsync(Guid id);
    
    Task<List<Subscription>> ListAsync();
    
    void Update(Subscription subscription);
    
    void RemoveSubscription(Subscription subscription);
}