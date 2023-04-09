using System;
using FluentMigrator;
using FluentMigrator.Builders.Create.Table;

namespace Webinex.Migrations
{
    public static class CreateTableWithColumnSyntaxExtensions
    {
        public static ICreateTableWithColumnSyntax WithColumn(
            this ICreateTableWithColumnSyntax tableBuilder,
            string columnName,
            Action<ICreateTableColumnAsTypeSyntax> colAction)
        {
            var columnBuilder = tableBuilder.WithColumn(columnName);
            colAction(columnBuilder);

            return tableBuilder;
        }

        public static ICreateTableWithColumnSyntax WhenDb(
            this ICreateTableWithColumnSyntax table,
            Migration migration,
            string database,
            Action<ICreateTableWithColumnSyntax> then,
            Action<ICreateTableWithColumnSyntax> @else = null)
        {
            migration.IfDatabase(
                database,
                () => then(table),
                @else != null ? () => @else(table) : null);
            return table;
        }

        public static ICreateTableWithColumnSyntax WhenNotDb(
            this ICreateTableWithColumnSyntax table,
            Migration migration,
            string database,
            Action<ICreateTableWithColumnSyntax> then)
        {
            migration.IfNotDatabase(
                database,
                () => then(table));
            return table;
        }

        public static ICreateTableWithColumnSyntax WhenSQLite(
            this ICreateTableWithColumnSyntax table,
            Migration migration,
            Action<ICreateTableWithColumnSyntax> then,
            Action<ICreateTableWithColumnSyntax> @else = null)
        {
            migration.IfDatabase(
                Databases.SQLITE,
                () => then(table),
                @else != null ? () => @else(table) : null);
            return table;
        }

        public static ICreateTableWithColumnSyntax WhenNotSQLite(
            this ICreateTableWithColumnSyntax table,
            Migration migration,
            Action<ICreateTableWithColumnSyntax> then)
        {
            migration.IfNotDatabase(
                Databases.SQLITE,
                () => then(table));
            return table;
        }
    }
}