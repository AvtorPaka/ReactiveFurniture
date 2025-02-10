using FluentMigrator;
using FluentMigrator.Postgres;

namespace Management.Service.Infrastructure.Dal.Migrations;

[Migration(version: 20250202, TransactionBehavior.Default)]
public class InitSchema: Migration 
{
    public override void Up()
    {
        Create.Table("furniture_goods")
            .WithColumn("id").AsInt64().PrimaryKey("furniture_goods_pk").Identity()
            .WithColumn("name").AsString(255).NotNullable()
            .WithColumn("price").AsDecimal(19, 5).NotNullable()
            .WithColumn("release_date").AsDate();
    }

    public override void Down()
    {
        Delete.Table("furniture_goods");
    }
}