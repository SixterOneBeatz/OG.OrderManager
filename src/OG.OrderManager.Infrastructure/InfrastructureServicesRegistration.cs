using Microsoft.Extensions.DependencyInjection;
using OG.OrderManager.Application.Common.Interfaces;
using OG.OrderManager.Infrastructure.Repositories;

namespace OG.OrderManager.Infrastructure
{
    public static class InfrastructureServicesRegistration
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IGreetRepository, GreetRepository>();

            return services;
        }
    }
}
