﻿using GymManagement.Application.Common.Interfaces;
using GymManagement.Domain.Admins;
using GymManagement.Infrastructure.Common.Persistence;
using Microsoft.EntityFrameworkCore;

namespace GymManagement.Infrastructure.Admins.Persistence;

public class AdminsesRepository : IAdminsRepository
{
    private readonly GymManagementDbContext _dbContext;

    public AdminsesRepository(GymManagementDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<Admin?> GetByIdAsync(Guid adminId)
    {
        return await _dbContext.Admins.FirstOrDefaultAsync(x => x.Id == adminId);
    }

    public void Update(Admin admin)
    {
        _dbContext.Update(admin);
    }
}