using GymManagement.Application.Common.Interfaces;
using GymManagement.Domain.Gyms;
using GymManagement.Infrastructure.Common.Persistence;

namespace GymManagement.Infrastructure.Gyms.Persistence;

public class GymsRepository : IGymsRepository
{
    private readonly GymManagementDbContext _gymManagementDbContext;

    public GymsRepository(GymManagementDbContext gymManagementDbContext)
    {
        _gymManagementDbContext = gymManagementDbContext;
    }
    
    public async Task AddGymAsync(Gym gym)
    {
        await _gymManagementDbContext.Gyms.AddAsync(gym);
    }

    public Task<Gym?> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> ExistsAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<List<Gym>> ListBySubscriptionIdAsync(Guid subscriptionId)
    {
        throw new NotImplementedException();
    }

    public Task UpdateGymAsync(Gym gym)
    {
        throw new NotImplementedException();
    }

    public Task RemoveGymAsync(Gym gym)
    {
        throw new NotImplementedException();
    }

    public Task RemoveRangeAsync(List<Gym> gyms)
    {
        throw new NotImplementedException();
    }
}