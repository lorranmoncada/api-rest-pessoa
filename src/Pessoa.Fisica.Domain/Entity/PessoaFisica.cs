using Pessoa.Core.DomainObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pessoa.Fisica.Domain.Entity
{
    public class PessoaFisica : BaseEntity
    {
       
        public string Nome { get; private set; }
        public string Cpf { get; private set; }
        public string Endereco { get; private set; }
        public string Contato { get; private set; }
        public char Ativo { get; private set; }

        public PessoaFisica(string nome, string cpf, string endereco, string contato, char ativo)
        {
            Nome = nome;
            Cpf = cpf;
            Endereco = endereco;
            Contato = contato;
            Ativo = ativo;
        }

        public void Inativar() => Ativo = 'N';
              
    }
}
