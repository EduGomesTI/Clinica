namespace Clinica.Doctors.Infrastructure.Options
{
    public sealed class DatabaseOptions
    {
        public string ConnectionString { get; set; } = string.Empty;

        public bool EnabledDetailedErrors { get; set; }

        public bool EnabledSensitiveDataLogging { get; set; }

        public int MaxRetryCount { get; set; }

        public int CommandTimeOut { get; set; }

        public string? MigrationHistoryTable { get; set; }
    }
}