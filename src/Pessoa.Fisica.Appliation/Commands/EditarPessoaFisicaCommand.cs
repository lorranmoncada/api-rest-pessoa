using FluentValidation;
using Pessoa.Core.Message;
using Pessoa.Core.Util;
using System;

namespace Pessoa.Fisica.Appliation.Commands
{
    public class EditarPessoaFisicaCommand : Command
    {
        public Guid Id { get; private set; }
        public string Nome { get; private set; }
        public string Cpf { get; private set; }
        public string Endereco { get; private set; }
        public string Contato { get; private set; }
        public char Ativo { get; private set; }

        public EditarPessoaFisicaCommand(Guid id,string nome, string cpf, string endereco, string contato, char ativo)
        {
            Id = id;
            Nome = nome;
            Cpf = cpf;
            Endereco = endereco;
            Contato = contato;
            Ativo = ativo;
        }

        public override bool EhValido()
        {
            ValidationResult = new EditarPessoaFisicaValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class EditarPessoaFisicaValidation : AbstractValidator<EditarPessoaFisicaCommand>
    {
        public EditarPessoaFisicaValidation()
        {
            RuleFor(p => p.Id).NotEmpty().NotNull().WithMessage("Id da pessoa é física obrigatório");
            RuleFor(p => p.Nome).NotEmpty().NotNull().WithMessage("Nome obrigatório");
            RuleFor(p => p.Endereco).NotEmpty().NotNull().WithMessage("Endereço obrigatório");
            RuleFor(p => p.Contato).NotEmpty().NotNull().WithMessage("Nome obrigatório");
            RuleFor(p => p.Cpf).Must(ValidateCpf.IsCpf).WithMessage("Formato de cpf inválido");
            RuleFor(p => p.Ativo).Must(VerifyChar).WithMessage("Pessoa Cadastrada deve estar ativa ou inativa");
        }

        private bool VerifyChar(char value)
        {
            return value == 'S' || value == 'N';
        }
    }
}
