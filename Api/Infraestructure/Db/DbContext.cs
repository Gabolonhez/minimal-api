using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using minimal_api.Domain.Entities;
using minimal_api.Domain.Enums; // Add this using

namespace minimal_api.Infraestructure.Db
{
    public class ApplicationDbContext : DbContext
    {
        private readonly IConfiguration? _configuration;

        // Constructor usado em tempo de execução/design-time pelo EF (padrão recomendado)
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IConfiguration? configuration = null)
            : base(options)
        {
            _configuration = configuration;
        }

        // Fixed typo: Admnistrators -> Administrators
        public DbSet<Administrator> Administrators { get; set; } = null!;

        public DbSet<Vehicle> Vehicles { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Administrator>().HasData(
                new Administrator
                {
                    Id = 1,
                    Email = "administrator@test.com", // Fixed typo
                    Password = "123456",
                    Profile = Profile.Adm // Use enum value instead of string
                }
            );
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Só tenta configurar aqui se não foi configurado via DI e se temos configuração disponível
            if (!optionsBuilder.IsConfigured && _configuration != null)
            {
                var connectionString = _configuration.GetConnectionString("Mysql");
                if (!string.IsNullOrEmpty(connectionString))
                {
                    optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
                }
            }
        }
    }
}