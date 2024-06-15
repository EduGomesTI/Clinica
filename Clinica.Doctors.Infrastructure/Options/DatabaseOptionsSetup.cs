﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Clinica.Doctors.Infrastructure.Options
{
    public sealed class DatabaseOptionsSetup : IConfigureOptions<DatabaseOptions>
    {
        private readonly IConfiguration _configuration;
        private const string ConfigurationSectionName = "DatabaseOptions";

        public DatabaseOptionsSetup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Configure(DatabaseOptions options)
        {
            var connectionString = _configuration.GetConnectionString("Database");

            options.ConnectionString = connectionString!;

            _configuration.GetSection(ConfigurationSectionName).Bind(options);
        }
    }
}