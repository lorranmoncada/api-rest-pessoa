using FluentValidation;
using Pessoa.Core.Message;
using Pessoa.Core.Util;
using System;

namespace Pessoa.Juridica.Appliation.Commands
{
    public class EditarPessoaJuridicaCommand : Command
    {
        public Guid Id { get; private set; }
        public string RazaoSocial { get; private set; }
        public string Cnpj { get; private set; }
        public string Endereco { get; private set; }
        public string Contato { get; private set; }
        public char Ativo { get; private set; }

        public EditarPessoaJuridicaCommand(Guid id,string razaoSocial, string cnpj, string endereco, string contato, char ativo)
        {
            Id = id;
            RazaoSocial = razaoSocial;
            Cnpj = cnpj;
            Endereco = endereco;
            Contato = contato;
            Ativo = ativo;
        }

        public override bool EhValido()
        {
            ValidationResult = new EditarPessoaJuridicaValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class EditarPessoaJuridicaValidation : AbstractValidator<EditarPessoaJuridicaCommand>
    {
        public EditarPessoaJuridicaValidation()
        {
            RuleFor(p => p.Id).NotEmpty().NotNull().WithMessage("Id da pessoa jurídica é física obrigatório");
            RuleFor(p => p.RazaoSocial).NotEmpty().NotNull().WithMessage("Razão social obrigatório");
            RuleFor(p => p.Endereco).NotEmpty().NotNull().WithMessage("Endereço obrigatório");
            RuleFor(p => p.Contato).NotEmpty().NotNull().WithMessage("Razão social obrigatório");
            RuleFor(p => p.Cnpj).Must(ValidateCnpj.IsCnpj).WithMessage("Formato de cnpj inválido");
            RuleFor(p => p.Ativo).Must(VerifyChar).WithMessage("Pessoa Jurídica cadastrada deve estar ativa ou inativa");
        }

        private bool VerifyChar(char value)
        {
            return value == 'S' || value == 'N';
        }
    }
}
