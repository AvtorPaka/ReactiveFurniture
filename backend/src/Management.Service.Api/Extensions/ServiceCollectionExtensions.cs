using Management.Service.Api.FiltersAttributes;

namespace Management.Service.Api.Extensions;

internal static class ServiceCollectionExtensions
{
    internal static IServiceCollection AddAuthFilter(this IServiceCollection services)
    {
        services.AddScoped<SessionAuthFilter>();
        return services;
    }
}