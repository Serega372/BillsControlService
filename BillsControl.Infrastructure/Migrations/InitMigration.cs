using System.Data;
using BillsControl.ApplicationCore.Abstract.Auth;
using BillsControl.ApplicationCore.Entities;
using BillsControl.Infrastructure.AuthHelpers;
using FluentMigrator;

namespace BillsControl.Infrastructure.Migrations;

[Migration(202509231900)]
public class InitMigration : Migration
{
    public override void Up()
    {
        Create.Schema("public");
        CreateTablePersonalBills();
        CreateTableResidents();
        CreateTableUsers();
        CreateResidentsBillIdForeignKey();
        CreateAdminEntity();
    }

    public override void Down()
    {
        Delete.ForeignKey("residents_bill_id_fkey").OnTable("residents");
        Delete.Table("users");
        Delete.Table("residents");
        Delete.Table("personal_bills");
    }

    private void CreateTablePersonalBills()
    {
        Create.Table("personal_bills")
            .InSchema("public")
            .WithColumn("id").AsGuid().NotNullable().PrimaryKey()
            .WithColumn("bill_number").AsString(10).NotNullable().Unique()
            .WithColumn("open_date").AsDate().NotNullable()
            .WithColumn("close_date").AsDate().Nullable()
            .WithColumn("is_closed").AsBoolean().NotNullable()
            .WithColumn("address").AsString(100).Nullable()
            .WithColumn("place_area").AsFloat().NotNullable();
    }

    private void CreateTableResidents()
    {
        Create.Table("residents")
            .InSchema("public")
            .WithColumn("id").AsGuid().NotNullable().PrimaryKey()
            .WithColumn("personal_bill_number").AsString(10).Nullable()
            .WithColumn("first_name").AsString(35).NotNullable()
            .WithColumn("last_name").AsString(35).NotNullable()
            .WithColumn("middle_name").AsString(35).Nullable()
            .WithColumn("personal_bill_id").AsGuid().Nullable()
            .WithColumn("is_owner").AsBoolean().NotNullable();
    }

    private void CreateTableUsers()
    {
        Create.Table("users")
            .InSchema("public")
            .WithColumn("id").AsGuid().NotNullable().PrimaryKey()
            .WithColumn("username").AsString(35).NotNullable()
            .WithColumn("password_hash").AsString().NotNullable()
            .WithColumn("role").AsInt32().NotNullable();
    }

    private void CreateResidentsBillIdForeignKey()
    {
        Create.ForeignKey("residents_bill_id_fkey")
            .FromTable("residents").InSchema("public").ForeignColumn("personal_bill_id")
            .ToTable("personal_bills").InSchema("public").PrimaryColumn("id").OnDelete(Rule.SetNull);
    }

    private void CreateAdminEntity()
    {
        Insert.IntoTable("users")
            .InSchema("public")
            .Row(new
            {
                id = "7a9f1d6d-f7fe-49d1-b918-ea0fda7156ab",
                username = "admin",
                password_hash = "$2a$11$9yz7sHlJH/4r4903TsktvOiFFLyeV3ftMVBFuSdTIHVA53ZzWD.Bm",
                role = 0
            });
    }
}