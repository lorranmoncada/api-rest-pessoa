

using Pessoa.Core.DomainObject;

namespace Pessoa.Juridica.Domain.Entity
{
    public class PessoaJuridica : BaseEntity
    {
       
        public string RazaoSocial { get; private set; }
        public string Cnpj { get; private set; }
        public string Endereco { get; private set; }
        public string Contato { get; private set; }
        public char Ativo { get; private set; }

        public PessoaJuridica(string razaoSocial, string cnpj, string endereco, string contato, char ativo)
        {
            RazaoSocial = razaoSocial;
            Cnpj = cnpj;
            Endereco = endereco;
            Contato = contato;
            Ativo = ativo;
        }

        public void Inativar() => Ativo = 'N';
              
    }
}
