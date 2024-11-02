using GymManagement.Application.Common.Interfaces;
using GymManagement.Infrastructure.Admins.Persistence;
using GymManagement.Infrastructure.Common.Persistence;
using GymManagement.Infrastructure.Gyms.Persistence;
using GymManagement.Infrastructure.Subscriptions;
using GymManagement.Infrastructure.Subscriptions.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace GymManagement.Infrastructure.IoC;

public static class InfrastructureModule
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddScoped<ISubscriptionsRepository, SubscriptionsRepository>();
        services.AddScoped<IGymsRepository, GymsRepository>();
        services.AddScoped<IAdminRepository, AdminsRepository>();
        services.AddDbContext<GymManagementDbContext>(options => options.UseSqlite("Data Source=GymManagement.db"));
        services.AddScoped<IUnitOfWork>(serviceProvider => serviceProvider.GetRequiredService<GymManagementDbContext>());
        return services;
    }
}