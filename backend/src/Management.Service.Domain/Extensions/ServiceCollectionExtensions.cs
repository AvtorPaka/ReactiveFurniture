using Management.Service.Domain.Services;
using Management.Service.Domain.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Management.Service.Domain.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDomain(this IServiceCollection services)
    {
        services.AddScoped<IFurnitureGoodsService, FurnitureGoodsService>();
        services.AddScoped<IUserCredentialsService, UserCredentialsService>();

        return services;
    }
}