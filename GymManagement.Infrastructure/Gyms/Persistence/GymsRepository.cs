using GymManagement.Application.Common.Interfaces;
using GymManagement.Domain.Gyms;
using GymManagement.Infrastructure.Common.Persistence;
using Microsoft.EntityFrameworkCore;

namespace GymManagement.Infrastructure.Gyms.Persistence;

public class GymsRepository : IGymsRepository
{
    private readonly GymManagementDbContext _dbContext;

    public GymsRepository(GymManagementDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task AddGymAsync(Gym gym)
    {
        await _dbContext.Gyms.AddAsync(gym);
    }

    public async Task<Gym?> GetByIdAsync(Guid id)
    {
        return await _dbContext.Gyms.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<bool> ExistsAsync(Guid id)
    {
        return await _dbContext.Gyms.AsNoTracking().AnyAsync(x => x.Id == id);
    }

    public async Task<List<Gym>> ListBySubscriptionIdAsync(Guid subscriptionId)
    {
        return await _dbContext.Gyms.Where(x => x.SubscriptionId == subscriptionId).ToListAsync();
    }

    public void UpdateGym(Gym gym)
    {
        _dbContext.Update(gym);
    }

    public void RemoveGym(Gym gym)
    {
        _dbContext.Remove(gym);
    }

    public void RemoveRange(List<Gym> gyms)
    {
        _dbContext.RemoveRange(gyms);
    }
}