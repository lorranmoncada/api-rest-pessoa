using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pessoa.Juridica.Domain.Entity;

namespace Pessoa.Fisica.Infraestructure.Mapping
{
    public class PessoaJuridicaMap : IEntityTypeConfiguration<PessoaJuridica>
    {
        public void Configure(EntityTypeBuilder<PessoaJuridica> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).HasColumnName("id").IsRequired();
            builder.Property(c => c.RazaoSocial).HasColumnName("razao_social").HasMaxLength(150).IsRequired();
            builder.Property(c => c.Cnpj).HasColumnName("cnpj").HasMaxLength(50).IsRequired();
            builder.Property(c => c.Contato).HasColumnName("contato").HasMaxLength(300).IsRequired();
            builder.Property(c => c.Endereco).HasColumnName("endereco").HasMaxLength(11).IsRequired();
            builder.Property(c => c.Ativo).HasColumnName("ativo").HasMaxLength(1).IsRequired();

            builder.ToTable("pessoajuridica");
        }
    }
}
