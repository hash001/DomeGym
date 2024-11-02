using ErrorOr;
using FluentValidation;
using GymManagement.Application.Common.Behaviours;
using GymManagement.Application.Gyms.Commands.CreateGym;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace GymManagement.Application.IoC;

public static class ApplicationModule
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddMediatR(options =>
        {
            options.RegisterServicesFromAssemblyContaining(typeof(ApplicationModule));
            options
                .AddOpenBehavior(typeof(ValidationBehaviour<,>));
        });

        serviceCollection.AddValidatorsFromAssemblyContaining(typeof(ApplicationModule));
        return serviceCollection;
    }
}