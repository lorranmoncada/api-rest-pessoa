

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Pessoa.Fisica.Domain.Entity;

namespace Pessoa.Fisica.Infraestructure
{
    public class PessoaFisicaContext : DbContext
    {
        // pesquisar
        private readonly IConfiguration _configuration;
        public PessoaFisicaContext(IConfiguration configuration, DbContextOptions<PessoaFisicaContext> options)
          : base(options) { _configuration = configuration; }

        public DbSet<PessoaFisica> PessoaFisica { get;set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PessoaFisicaContext).Assembly);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // pesquisar
            optionsBuilder.UseNpgsql(_configuration.GetConnectionString("DefaultConnection"), options => options.EnableRetryOnFailure());
        }
    }
}
