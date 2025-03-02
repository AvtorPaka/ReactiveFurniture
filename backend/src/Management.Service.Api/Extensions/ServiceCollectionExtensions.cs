using Management.Service.Api.BackgroundServices;
using Management.Service.Api.FiltersAttributes;

namespace Management.Service.Api.Extensions;

internal static class ServiceCollectionExtensions
{
    internal static IServiceCollection AddAuthFilter(this IServiceCollection services)
    {
        services.AddScoped<SessionAuthFilter>();
        return services;
    }

    internal static IServiceCollection AddGoodsFakerService(this IServiceCollection services)
    {
        services.AddHostedService<FakeGoodsHostedService>();

        return services;
    }

    internal static IServiceCollection AddCustomCors(this IServiceCollection services)
    {
        string clientPort = Environment.GetEnvironmentVariable("CLIENT_PORT") ?? "3000";
        string[] clientOrigins = [$"http://host.docker.internal:{clientPort}", $"http://localhost:{clientPort}", $"http://172.17.0.1:{clientPort}"]; 
            
        services.AddCors(options =>
        {
            options.AddDefaultPolicy(policy =>
            {
                policy
                    .WithOrigins(clientOrigins)
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
            });
        });
        return services;
    }
}