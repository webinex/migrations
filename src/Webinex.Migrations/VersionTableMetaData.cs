using FluentMigrator.Runner.VersionTableInfo;

namespace Webinex.Migrations
{
    public class VersionTableMetaData : IVersionTableMetaData
    {
        public VersionTableMetaData()
        {
#pragma warning disable 618
            var defaultValues = new DefaultVersionTableMetaData();
            ApplicationContext = defaultValues.ApplicationContext;
#pragma warning restore 618

            OwnsSchema = defaultValues.OwnsSchema;
            SchemaName = defaultValues.SchemaName;
            TableName = defaultValues.TableName;
            ColumnName = defaultValues.ColumnName;
            DescriptionColumnName = defaultValues.DescriptionColumnName;
            UniqueIndexName = defaultValues.UniqueIndexName;
            AppliedOnColumnName = defaultValues.AppliedOnColumnName;
        }
        
        public object ApplicationContext { get; set; }
        public bool OwnsSchema { get; set; }
        public string SchemaName { get; set; }
        public string TableName { get; set; }
        public string ColumnName { get; set; }
        public string DescriptionColumnName { get; set; }
        public string UniqueIndexName { get; set; }
        public string AppliedOnColumnName { get; set; }
    }
}