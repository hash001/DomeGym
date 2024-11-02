using GymManagement.Application.Common.Interfaces;
using GymManagement.Domain.Subscriptions;
using GymManagement.Infrastructure.Common.Persistence;
using Microsoft.EntityFrameworkCore;

namespace GymManagement.Infrastructure.Subscriptions.Persistence;

public class SubscriptionsRepository : ISubscriptionsRepository
{
    private readonly GymManagementDbContext _dbContext;

    public SubscriptionsRepository(GymManagementDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(Subscription subscription)
    {
        await _dbContext.AddAsync(subscription);
    }

    public async Task<bool> ExistsAsync(Guid id)
    {
        return await _dbContext.Subscriptions.AsNoTracking().AnyAsync(x => x.Id == id);
    }

    public async Task<Subscription?> GetByIdAsync(Guid id)
    {
        return await _dbContext.Subscriptions.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<List<Subscription>> ListAsync()
    {
        return await _dbContext.Subscriptions.ToListAsync();
    }

    public void Update(Subscription subscription)
    {
        _dbContext.Update(subscription);
    }

    public void RemoveSubscription(Subscription subscription)
    {
        _dbContext.Subscriptions.Remove(subscription);
    }
}