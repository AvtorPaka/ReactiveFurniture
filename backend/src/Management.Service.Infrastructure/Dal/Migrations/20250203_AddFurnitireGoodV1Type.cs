using FluentMigrator;

namespace Management.Service.Infrastructure.Dal.Migrations;

[Migration(version: 20250203, TransactionBehavior.Default)]
public class AddFurnitureGoodV1Type: Migration
{
    public override void Up()
    {
        const string sql = @"
DO $$ 
    BEGIN
        IF NOT EXISTS (SELECT 1 FROM pg_type WHERE typname = 'furniture_good_v1') THEN
            CREATE TYPE furniture_good_v1 as
            (
                id           bigint,
                name         varchar(255)
                price        numeric(19,5)
                release_date date
            );
        END IF;
    END 
$$;
";
    }

    public override void Down()
    {
        const string sql = @"
DO $$
    BEGIN
        DROP TYPE IF EXISTS calculations_v1;
    END
$$;
";
        Execute.Sql(sql);
    }
}