using System.Data;
using FluentMigrator;

namespace BillsControl.Infrastructure.Migrations;

[Migration(202509231900)]
public class InitMigration : Migration
{
    // private const string schema = "public";
    
    public override void Up()
    {
        if (!Schema.Schema("public").Exists())
        {
            Create.Schema("public");
        }

        if (!Schema.Schema("public").Table("personal_bills").Exists())
        {
            CreateTablePersonalBills();
        }
        
        if (!Schema.Schema("public").Table("residents").Exists())
        {
            CreateTableResidents();
        }

        if (!Schema.Schema("public").Table("residents").Constraint("residents_bill_id_fkey").Exists())
        {
            Create.ForeignKey("residents_bill_id_fkey")
                .FromTable("residents").InSchema("public").ForeignColumn("personal_bill_id")
                .ToTable("personal_bills").InSchema("public").PrimaryColumn("id").OnDelete(Rule.SetNull);
        }
    }

    public override void Down()
    {
        if (Schema.Table("personal_bills").Exists())
            Delete.Table("personal_bills");
        
        if (Schema.Table("residents").Exists())
            Delete.Table("residents");
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
}