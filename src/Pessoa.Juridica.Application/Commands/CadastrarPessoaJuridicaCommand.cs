using FluentValidation;
using Pessoa.Core.Message;
using Pessoa.Core.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pessoa.Juridica.Appliation.Commands
{
    public class CadastrarPessoaJuridicaCommand : Command
    {
        public string RazaoSocial { get; private set; }
        public string Cnpj { get; private set; }
        public string Endereco { get; private set; }
        public string Contato { get; private set; }
        public char Ativo { get; private set; }

        public CadastrarPessoaJuridicaCommand(string nome, string cpf, string endereco, string contato, char ativo)
        {
            RazaoSocial = nome;
            Cnpj = cpf;
            Endereco = endereco;
            Contato = contato;
            Ativo = ativo;
        }

        public override bool EhValido()
        {
            ValidationResult = new CadastrarPessoaJuridicaValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class CadastrarPessoaJuridicaValidation : AbstractValidator<CadastrarPessoaJuridicaCommand>
    {
        public CadastrarPessoaJuridicaValidation()
        {
            RuleFor(p => p.RazaoSocial).NotEmpty().NotNull().WithMessage("Razao social obrigatório");
            RuleFor(p => p.Endereco).NotEmpty().NotNull().WithMessage("Endereço obrigatório");
            RuleFor(p => p.Contato).NotEmpty().NotNull().WithMessage("Contato obrigatório");
            RuleFor(p => p.Cnpj).Must(ValidateCnpj.IsCnpj).WithMessage("Formato de cnpj inválido");
            RuleFor(p => p.Ativo).Equal('S').NotEmpty().NotNull().WithMessage("Pessoa Jurídica Cadastrada deve estar ativa (S)");
            RuleFor(p => p.Ativo).NotEmpty().NotNull().WithMessage("Pessoa Jurídica Cadastrada deve estar ativa");
        }
    }
}
