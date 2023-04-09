using System;
using FluentMigrator;
using FluentMigrator.Builders.Alter.Table;

namespace Webinex.Migrations
{
    public static class AlterTableAddColumnOrAlterColumnSyntaxExtensions
    {
        public static IAlterTableAddColumnOrAlterColumnSyntax AddColumn(
            this IAlterTableAddColumnOrAlterColumnSyntax tableBuilder,
            string columnName,
            Action<IAlterTableColumnAsTypeSyntax> colAction)
        {
            var columnBuilder = tableBuilder.AddColumn(columnName);
            colAction(columnBuilder);

            return tableBuilder;
        }

        public static IAlterTableAddColumnOrAlterColumnSyntax WhenDb(
            this IAlterTableAddColumnOrAlterColumnSyntax table,
            Migration migration,
            string database,
            Action<IAlterTableAddColumnOrAlterColumnSyntax> then,
            Action<IAlterTableAddColumnOrAlterColumnSyntax> @else = null)
        {
            migration.IfDatabase(
                database,
                () => then(table),
                @else != null ? () => @else(table) : null);
            return table;
        }

        public static IAlterTableAddColumnOrAlterColumnSyntax WhenNotDb(
            this IAlterTableAddColumnOrAlterColumnSyntax table,
            Migration migration,
            string database,
            Action<IAlterTableAddColumnOrAlterColumnSyntax> then)
        {
            migration.IfNotDatabase(
                database,
                () => then(table));
            return table;
        }

        public static IAlterTableAddColumnOrAlterColumnSyntax WhenSQLite(
            this IAlterTableAddColumnOrAlterColumnSyntax table,
            Migration migration,
            Action<IAlterTableAddColumnOrAlterColumnSyntax> then,
            Action<IAlterTableAddColumnOrAlterColumnSyntax> @else = null)
        {
            migration.IfDatabase(
                Databases.SQLITE,
                () => then(table),
                @else != null ? () => @else(table) : null);
            return table;
        }

        public static IAlterTableAddColumnOrAlterColumnSyntax WhenNotSQLite(
            this IAlterTableAddColumnOrAlterColumnSyntax table,
            Migration migration,
            Action<IAlterTableAddColumnOrAlterColumnSyntax> then)
        {
            migration.IfNotDatabase(
                Databases.SQLITE,
                () => then(table));
            return table;
        }
    }
}