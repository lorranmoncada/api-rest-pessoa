using FluentValidation;
using Pessoa.Core.Message;
using System;


namespace Pessoa.Juridica.Appliation.Commands
{
    public class InativarPessoaJuridicaCommand : Command
    {
        public Guid Id { get; private set; }

        public InativarPessoaJuridicaCommand(Guid id)
        {
            Id = id;
        }

        public override bool EhValido()
        {
            ValidationResult = new InativarPessoaFisicaValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class InativarPessoaFisicaValidation : AbstractValidator<InativarPessoaJuridicaCommand>
    {
        public InativarPessoaFisicaValidation()
        {
            RuleFor(p => p.Id).NotEqual(Guid.Empty).NotEmpty().WithMessage("Id da pessoa jurídica é obrigatório");
        }
    }
}
