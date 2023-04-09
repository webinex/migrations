using System;
using FluentMigrator;

namespace Webinex.Migrations
{
    public static class MigrationBaseExtensions
    {
        public static void IfSQLite(
            this MigrationBase root,
            Action action,
            Action otherwise = default)
        {
            root.IfDatabase(Databases.SQLITE).Delegate(action);

            if (otherwise != null)
            {
                root.IfDatabase((db) => db != Databases.SQLITE).Delegate(otherwise);
            }
        }

        public static void IfNotSQLite(
            this MigrationBase root,
            Action action)
        {
            root.IfDatabase((db) => db != Databases.SQLITE).Delegate(action);
        }

        public static void IfDatabase(
            this MigrationBase root,
            string database,
            Action action,
            Action otherwise = default)
        {
            root.IfDatabase(database).Delegate(action);

            if (otherwise != null)
            {
                root.IfDatabase((db) => db != database).Delegate(otherwise);
            }
        }

        public static void IfNotDatabase(this MigrationBase root, string database, Action action)
        {
            root.IfDatabase((db) => db != database).Delegate(action);
        }
    }
}