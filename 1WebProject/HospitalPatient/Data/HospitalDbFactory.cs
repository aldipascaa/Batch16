
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace HospitalPatient.Data
{
    // Used ONLY by 'dotnet ef' at design time (migrations)
    public class HospitalDbFactory : IDesignTimeDbContextFactory<HospitalDb>
    {
        public HospitalDb CreateDbContext(string[] args)
        {
            // Build configuration (supports appsettings.json + appsettings.Development.json)
            var basePath = Directory.GetCurrentDirectory();
            var builder = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json", optional: true)
                .AddJsonFile("appsettings.Development.json", optional: true)
                .AddEnvironmentVariables();

            var config = builder.Build();

            // Read connection string
            var connString = config.GetConnectionString("Default");
            if (string.IsNullOrWhiteSpace(connString))
            {
                // Fallback to a deterministic path inside bin folder
                var dbPath = Path.Combine(AppContext.BaseDirectory, "hospital.db");
                connString = $"Data Source={dbPath}";
            }

            var options = new DbContextOptionsBuilder<HospitalDb>()
                .UseSqlite(connString)
                .Options;

            return new HospitalDb(options);
        }
    }
}
