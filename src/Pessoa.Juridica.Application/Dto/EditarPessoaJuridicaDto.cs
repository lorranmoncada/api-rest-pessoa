using System;

namespace Pessoa.Juridica.Appliation.Dto
{
    public class EditarPessoaJuridicaDto
    {
        public Guid Id { get; set; }
        public string RazaoSocial { get; set; }
        public string Cnpj { get; set; }
        public string Endereco { get; set; }
        public string Contato { get; set; }
        public bool Ativo { get; set; }
    }
}
