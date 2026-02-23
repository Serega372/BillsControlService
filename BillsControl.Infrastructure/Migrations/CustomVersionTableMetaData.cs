using FluentMigrator.Runner.VersionTableInfo;

namespace BillsControl.Infrastructure.Migrations;

public class CustomVersionTableMetaData : IVersionTableMetaData
{
    public string TableName => "version_info";
    public string ColumnName => "version";
    public string DescriptionColumnName => "description";
    public string UniqueIndexName => "unique_index";
    public string AppliedOnColumnName => "applied_on";
    public bool CreateWithPrimaryKey => false;
    public bool OwnsSchema => false;
    public string SchemaName => "migrations";
}