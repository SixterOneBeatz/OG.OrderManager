using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OG.OrderManager.Application.Common.Interfaces;
using OG.OrderManager.Infrastructure.Contexts;

namespace OG.OrderManager.Infrastructure
{
    public static class InfrastructureServicesRegistration
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<OrderManagerContext>(opts => opts.UseSqlServer(configuration.GetConnectionString("OrderManager")));
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
