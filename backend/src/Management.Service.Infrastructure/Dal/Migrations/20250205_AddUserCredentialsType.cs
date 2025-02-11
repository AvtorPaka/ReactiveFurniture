using FluentMigrator;

namespace Management.Service.Infrastructure.Dal.Migrations;

[Migration(version:20250205, TransactionBehavior.Default)]
public class AddUserCredentialsType: Migration 
{
    
    public override void Up()
    {
        const string sql = @"
DO $$ 
    BEGIN
        IF NOT EXISTS (SELECT 1 FROM pg_type WHERE typname = 'user_credentials_v1') THEN
            CREATE TYPE user_credentials_v1 as
            (
                id           bigint,
                username     varchar(100),
                email        varchar(100),
                password     varchar(100)
            );
        END IF;
    END 
$$;
";
        Execute.Sql(sql);
    }

    public override void Down()
    {
        const string sql = @"
DO $$
    BEGIN
        DROP TYPE IF EXISTS user_credentials_v1;
    END
$$;
";
        Execute.Sql(sql);
    }
}