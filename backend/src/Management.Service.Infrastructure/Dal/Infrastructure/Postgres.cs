using Dapper;
using FluentMigrator.Runner;
using Management.Service.Domain.Contracts.Dal.Entities;
using Management.Service.Infrastructure.Configuration.Models;
using Management.Service.Infrastructure.Dal.TypeHandlers;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using Npgsql.NameTranslation;

namespace Management.Service.Infrastructure.Dal.Infrastructure;

public static class Postgres
{
    private static readonly INpgsqlNameTranslator Translator = new NpgsqlSnakeCaseNameTranslator();

    public static void ConfigureTypeMapOptions()
    {
        Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
        SqlMapper.AddTypeHandler(new SqlDateOnlyTypeHandler());
    }

    public static void AddDataSource(IServiceCollection services, PostgreConnectionOptions connectionOptions,
        bool isDevelopment)
    {
        services.AddNpgsqlDataSource(
            connectionString: connectionOptions.ConnectionString,
            builder =>
            {
                builder.MapComposite<UserCredentialEntity>("user_credentials_v1", Translator);
                builder.MapComposite<UserSessionEntity>("user_session_v1", Translator);
                builder.MapComposite<FurnitureGoodEntity>("furniture_good_v1", Translator);
                if (isDevelopment)
                {
                    builder.EnableParameterLogging();
                }
            }
        );
    }

    public static void AddMigrations(IServiceCollection services, PostgreConnectionOptions connectionOptions)
    {
        services.AddFluentMigratorCore()
            .ConfigureRunner(r => r
                .AddPostgres()
                .WithGlobalConnectionString(s => connectionOptions.ConnectionString)
                .ScanIn(typeof(Postgres).Assembly).For.Migrations()
            );
    }
}