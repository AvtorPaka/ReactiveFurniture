using Management.Service.Domain.Contracts.Dal.Interfaces;
using Management.Service.Infrastructure.Configuration.Models;
using Management.Service.Infrastructure.Dal.Infrastructure;
using Management.Service.Infrastructure.Dal.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Management.Service.Infrastructure.DependencyInjection.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDalInfrastructure(this IServiceCollection services,
        IConfiguration configuration)
    {
        var postgresConnectionSection = configuration.GetSection("DalOptions:PostgreOptions");

        PostgreConnectionOptions pgOptions = postgresConnectionSection.Get<PostgreConnectionOptions>() ??
                                             throw new ArgumentException("Postgre connection options is missing.");

        Postgres.AddDataSource(services, pgOptions);
        Postgres.ConfigureTypeMapOptions();
        Postgres.AddMigrations(services, pgOptions);
        
        return services;
    }

    public static IServiceCollection AddDalRepositories(this IServiceCollection services)
    {
        services.AddScoped<IFurnitureGoodRepository, FurnitureGoodRepository>();
        services.AddScoped<ICredentialsRepository, CredentialsRepository>();
        return services;
    }
}