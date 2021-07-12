using FluentValidation;
using Pessoa.Core.Message;
using Pessoa.Core.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pessoa.Fisica.Appliation.Commands
{
    public class CadastrarPessoaFisicaCommand : Command
    {
        public string Nome { get; private set; }
        public string Cpf { get; private set; }
        public string Endereco { get; private set; }
        public string Contato { get; private set; }
        public char Ativo { get; private set; }

        public CadastrarPessoaFisicaCommand(string nome, string cpf, string endereco, string contato, char ativo)
        {
            Nome = nome;
            Cpf = cpf;
            Endereco = endereco;
            Contato = contato;
            Ativo = ativo;
        }

        public override bool EhValido()
        {
            ValidationResult = new CadastrarPessoaFisicaValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class CadastrarPessoaFisicaValidation : AbstractValidator<CadastrarPessoaFisicaCommand>
    {
        public CadastrarPessoaFisicaValidation()
        {
            RuleFor(p => p.Nome).NotEmpty().NotNull().WithMessage("Nome obrigatório");
            RuleFor(p => p.Endereco).NotEmpty().NotNull().WithMessage("Endereço obrigatório");
            RuleFor(p => p.Contato).NotEmpty().NotNull().WithMessage("Nome obrigatório");
            RuleFor(p => p.Cpf).Must(ValidateCpf.IsCpf).WithMessage("Formato de cpf inválido");
            RuleFor(p => p.Ativo).Equal('S').NotEmpty().NotNull().WithMessage("Pessoa Cadastrada deve estar ativa (S)");
            RuleFor(p => p.Ativo).NotEmpty().NotNull().WithMessage("Pessoa Cadastrada deve estar ativa");
        }
    }
}
