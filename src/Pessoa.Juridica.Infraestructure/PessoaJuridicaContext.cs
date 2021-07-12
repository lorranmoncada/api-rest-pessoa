


using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Pessoa.Juridica.Domain.Entity;

namespace Pessoa.Juridica.Infraestructure
{
    public class PessoaJuridicaContext : DbContext
    {
        // pesquisar
        private readonly IConfiguration _configuration;
        public PessoaJuridicaContext(IConfiguration configuration, DbContextOptions<PessoaJuridicaContext> options)
          : base(options) { _configuration = configuration; }

        public DbSet<PessoaJuridica> PessoaJuridica { get;set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PessoaJuridicaContext).Assembly);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // pesquisar
            optionsBuilder.UseNpgsql(_configuration.GetConnectionString("DefaultConnection"), options => options.EnableRetryOnFailure());
        }
    }
}
