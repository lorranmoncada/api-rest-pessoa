using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pessoa.Fisica.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pessoa.Fisica.Infraestructure.Mapping
{
    public class PessoaFisicaMap : IEntityTypeConfiguration<PessoaFisica>
    {
        public void Configure(EntityTypeBuilder<PessoaFisica> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).HasColumnName("id").IsRequired();
            builder.Property(c => c.Nome).HasColumnName("nome").HasMaxLength(150).IsRequired();
            builder.Property(c => c.Cpf).HasColumnName("cpf").HasMaxLength(50).IsRequired();
            builder.Property(c => c.Contato).HasColumnName("contato").HasMaxLength(300).IsRequired();
            builder.Property(c => c.Endereco).HasColumnName("endereco").HasMaxLength(11).IsRequired();
            builder.Property(c => c.Ativo).HasColumnName("ativo").HasMaxLength(1).IsRequired();

            builder.ToTable("pessoafisica");
        }
    }
}
