using System.Net;
using System.Text.Json;
using Management.Service.Api.FiltersAttributes;
using Management.Service.Domain.Extensions;
using Management.Service.Infrastructure.DependencyInjection.Extensions;
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
            .AddDalInfrastructure(_configuration)
            .AddDalRepositories()
            .AddDomain()
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
        options.Filters.Add(new ExceptionFilterAttribute());
        options.Filters.Add(new ErrorResponseTypeAttribute((int)HttpStatusCode.NotFound));
        options.Filters.Add(new ErrorResponseTypeAttribute((int)HttpStatusCode.BadRequest));
        options.Filters.Add(new ErrorResponseTypeAttribute((int)HttpStatusCode.Unauthorized));
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