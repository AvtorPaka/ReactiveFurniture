using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

namespace Management.Service.Api;

public sealed class Startup
{
    private readonly IConfiguration _configuration;
    private readonly IWebHostEnvironment _hostEnvironment;

    public Startup(IConfiguration configuration, IWebHostEnvironment environment)
    {
        _configuration = configuration;
        _hostEnvironment = environment;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services
            .AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower;
            })
            .AddMvcOptions(ConfigureMvc);

        if (_hostEnvironment.IsDevelopment())
        {
            services.AddSwaggerGen(options => options.CustomSchemaIds(x => x.FullName));
        }
    }

    private static void ConfigureMvc(MvcOptions options)
    {
        
    }

    public void Configure(IApplicationBuilder app)
    {
        if (_hostEnvironment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        
        app.UseRouting();

        app.UseEndpoints(builder =>
        {
            builder.MapControllers();
        });
    }
}