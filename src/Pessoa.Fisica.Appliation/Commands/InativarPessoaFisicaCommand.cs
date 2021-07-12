using FluentValidation;
using Pessoa.Core.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pessoa.Fisica.Appliation.Commands
{
    public class InativarPessoaFisicaCommand : Command
    {
        public Guid Id { get; private set; }

        public InativarPessoaFisicaCommand(Guid id)
        {
            Id = id;
        }

        public override bool EhValido()
        {
            ValidationResult = new InativarPessoaFisicaValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class InativarPessoaFisicaValidation : AbstractValidator<InativarPessoaFisicaCommand>
    {
        public InativarPessoaFisicaValidation()
        {
            RuleFor(p => p.Id).NotEqual(Guid.Empty).NotEmpty().WithMessage("Id da pessoa é física obrigatório");
        }
    }
}
