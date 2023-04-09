using System;
using FluentMigrator.Runner;
using FluentMigrator.Runner.Initialization;
using Microsoft.Extensions.DependencyInjection;

namespace Webinex.Migrations
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMigrations(
            this IServiceCollection services,
            Action<FluentMigrationsConfiguration> configure)
        {
            services = services ?? throw new ArgumentNullException(nameof(services));
            configure = configure ?? throw new ArgumentNullException(nameof(configure));

            var configuration = new FluentMigrationsConfiguration();
            configure(configuration);
            configuration.Assert();

            services
                .AddFluentMigratorCore()
                .AddLogging(x => x.AddFluentMigratorConsole())
                .AddSingleton(configuration)
                .AddSingleton(new ConnectionStringReader(configuration))
                .AddSingleton<IConnectionStringReader>(x => x.GetRequiredService<ConnectionStringReader>())
                .AddSingleton<IMigrator, Migrator>()
                .Configure<RunnerOptions>(_ => {});

            services
                .ConfigureRunner(runner =>
                {
                    runner.WithVersionTable(configuration.VersionTable);
                    runner.ScanIn(configuration.Assemblies);

                    if (configuration.DefaultConnectionString != null)
                    {
                        runner.WithGlobalConnectionString(configuration.DefaultConnectionString);
                    }

                    if (configuration.ConfigureRunnerAction != null)
                    {
                        configuration.ConfigureRunnerAction(runner);
                    }
                });

            return services;
        }
    }
}