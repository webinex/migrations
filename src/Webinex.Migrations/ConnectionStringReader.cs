using System;
using FluentMigrator.Runner.Initialization;

namespace Webinex.Migrations
{
    internal class ConnectionStringReader : IConnectionStringReader
    {
        private readonly FluentMigrationsConfiguration _configuration;

        private string _overriden;

        public ConnectionStringReader(FluentMigrationsConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GetConnectionString(string _)
        {
            return _overriden ?? _configuration.DefaultConnectionString;
        }

        internal void Override(string value)
        {
            _overriden = value ?? throw new ArgumentNullException(nameof(value));
        }

        public int Priority { get; } = 300;
    }
}