using System;
using System.Linq;
using CommandLine;
using FluentMigrator.Runner;
using FluentMigrator.Runner.Initialization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Webinex.Migrations.Commands;

namespace Webinex.Migrations
{
    internal class Migrator : IMigrator
    {
        private readonly IServiceProvider _serviceProvider;

        public Migrator(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        private ConnectionStringReader ConnectionStringReader =>
            _serviceProvider.GetRequiredService<ConnectionStringReader>();

        private IMigrationRunner MigrationRunner => _serviceProvider.GetRequiredService<IMigrationRunner>();

        public void Run(string[] args)
        {
            Parser.Default.ParseArguments<PrintHowTo, MigrateCommand, RollbackCommand>(args)
                .MapResult(
                    (PrintHowTo _) => PrintHelp(), 
                    (MigrateCommand opt) => Migrate(opt),
                    (RollbackCommand opt) => Rollback(opt),
                    _ => 1);
        }

        private int PrintHelp()
        {
            Console.WriteLine(Parser.Default.FormatCommandLine(typeof(MigrateCommand)));

            return 0;
        }

        private int Rollback(RollbackCommand command)
        {
            OverrideTagsIfNotSpecified(command.Tags);
            OverrideConnectionStringIfSpecified(command.ConnectionString);
            MigrationRunner.ListMigrations();
            MigrationRunner.Rollback(command.Steps);

            return 0;
        }

        private int Migrate(MigrateCommand command)
        {
            OverrideTagsIfNotSpecified(command.Tags);
            OverrideConnectionStringIfSpecified(command.ConnectionString);
            MigrationRunner.ListMigrations();
            MigrationRunner.MigrateUp();
            
            return 0;
        }

        private void OverrideConnectionStringIfSpecified(string connectionString)
        {
            if (!string.IsNullOrWhiteSpace(connectionString))
                ConnectionStringReader.Override(connectionString);
        }

        private void OverrideTagsIfNotSpecified(string[] tags)
        {
            var options = _serviceProvider.GetRequiredService<IOptions<RunnerOptions>>();

            if (tags != null && (options.Value == null || options.Value.Tags?.Any() != true))
            {
                options.Value!.Tags = tags;
            }
        }
    }
}