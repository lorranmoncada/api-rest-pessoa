using System;

namespace Pessoa.Fisica.Appliation.Dto
{
    public class EditarPessoaFisicaDto
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Endereco { get; set; }
        public string Contato { get; set; }
        public bool Ativo { get; set; }
    }
}
