using FluentMigrator;
using FluentMigrator.Postgres;

namespace Management.Service.Infrastructure.Dal.Migrations;

[Migration(version: 20250204, TransactionBehavior.Default)]
public class AddCredentialsTables : Migration{
    
    public override void Up()
    {
        Create.Table("user_credentials")
            .WithColumn("id").AsInt64().PrimaryKey("user_credentials_pk").Identity()
            .WithColumn("username").AsString(100).NotNullable()
            .WithColumn("email").AsString(100).NotNullable()
            .WithColumn("password").AsString(100).NotNullable();

        Create.Table("user_sessions")
            .WithColumn("id").AsString(255).PrimaryKey("user_sessions_pk").Unique()
            .WithColumn("user_id").AsInt64().ForeignKey("user_credentials", "id").NotNullable()
            .WithColumn("expiration_date").AsDateTimeOffset().NotNullable();

        Create.Index("user_credentials_email_index")
            .OnTable("user_credentials")
            .OnColumn("email")
            .Unique();
    }

    public override void Down()
    {
        Delete.Table("user_sessions");
        Delete.Table("user_credentials");
    }
}