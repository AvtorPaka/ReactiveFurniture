using Management.Service.Domain.Contracts.Dal.Interfaces;
using Management.Service.Infrastructure.Dal.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Management.Service.Infrastructure.DependencyInjection.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDalInfrastructure(this IServiceCollection services,
        IConfiguration configuration)
    {
        return services;
    }

    public static IServiceCollection AddDalRepositories(this IServiceCollection services)
    {
        services.AddScoped<IFurnitureGoodRepository, FurnitureGoodRepository>();
        return services;
    }
}