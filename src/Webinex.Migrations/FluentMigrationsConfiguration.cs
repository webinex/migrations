using System;
using System.Linq;
using System.Reflection;
using FluentMigrator.Runner;
using FluentMigrator.Runner.VersionTableInfo;

namespace Webinex.Migrations
{
    public class FluentMigrationsConfiguration
    {
        private readonly VersionTableMetaData _versionTableMetaData = new VersionTableMetaData();
        
        internal string DefaultConnectionString { get; private set; }
        internal IVersionTableMetaData VersionTable => _versionTableMetaData;
        internal Assembly[] Assemblies { get; private set; }
        internal Action<IMigrationRunnerBuilder> ConfigureRunnerAction { get; private set; }

        public FluentMigrationsConfiguration UseAssembly(params Assembly[] assemblies)
        {
            if (Assemblies != null)
                throw new InvalidOperationException($"You might call {nameof(UseAssembly)} only once.");

            Assemblies = assemblies ?? throw new ArgumentNullException(nameof(assemblies));
            if (!Assemblies.Any())
                throw new ArgumentException("Might contain at least one assembly", nameof(assemblies));

            return this;
        }
        
        public FluentMigrationsConfiguration UseDefaultConnectionString(string connectionString)
        {
            if (DefaultConnectionString != null)
                throw new InvalidOperationException($"You might call {nameof(UseDefaultConnectionString)} only once.");
            
            DefaultConnectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
            return this;
        }

        public FluentMigrationsConfiguration ConfigureVersionTable(Action<VersionTableMetaData> configure)
        {
            configure = configure ?? throw new ArgumentNullException(nameof(configure));
            configure(_versionTableMetaData);

            return this;
        }

        public FluentMigrationsConfiguration ConfigureRunner(Action<IMigrationRunnerBuilder> configure)
        {
            ConfigureRunnerAction = configure ?? throw new ArgumentNullException(nameof(configure));
            return this;
        }

        internal void Assert()
        {
            if (Assemblies?.Any() != true)
                throw new InvalidOperationException("You might specify assemblies for migrations.");
        }
    }
}