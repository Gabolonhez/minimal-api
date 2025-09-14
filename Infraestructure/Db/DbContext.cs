using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using minimal_api.Domain.Entities;

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

        // (Opcional) mantém a propriedade DbSet
        public DbSet<Admnistrator> Admnistrators { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Só tenta configurar aqui se não foi configurado via DI e se temos configuração disponível
            if (!optionsBuilder.IsConfigured && _configuration != null)
            {
                var connectionString = _configuration.GetConnectionString("mysql");
                if (!string.IsNullOrEmpty(connectionString))
                {
                    optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
                }
            }
        }
    }
}