using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

namespace minimal_api.Infraestructure.Db
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var basePath = Directory.GetCurrentDirectory();
            var config = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: false)
                .AddEnvironmentVariables()
                .Build();

            var connectionString = config.GetConnectionString("mysql");

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();

            if (!string.IsNullOrEmpty(connectionString))
            {
                try
                {
                    // Use explicit server version to avoid AutoDetect connecting to the DB at design-time
                    var serverVersion = new MySqlServerVersion(new Version(8, 0, 33));
                    optionsBuilder.UseMySql(connectionString, serverVersion);
                }
                catch (Exception ex)
                {
                    // If something goes wrong, write a message and fall back to an empty options builder
                    Console.WriteLine($"DesignTimeDbContextFactory: falha ao configurar UseMySql: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("DesignTimeDbContextFactory: connection string 'mysql' não encontrada; verifique appsettings.json ou variáveis de ambiente.");
            }

            return new ApplicationDbContext(optionsBuilder.Options, config);
        }
    }
}