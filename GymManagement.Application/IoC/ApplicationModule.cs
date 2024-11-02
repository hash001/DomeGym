using Microsoft.Extensions.DependencyInjection;

namespace GymManagement.Application.IoC;

public static class ApplicationModule
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddMediatR(options =>
        {
            options.RegisterServicesFromAssemblyContaining(typeof(ApplicationModule));
        });
        return serviceCollection;
    }
}