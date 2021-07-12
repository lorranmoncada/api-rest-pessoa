using AutoMapper;
using MediatR;
using Pessoa.Core.Data;
using Pessoa.Core.DomainObject;
using Pessoa.Core.MediaTr;
using Pessoa.Core.Message;
using Pessoa.Core.Message.CommonMessages.Notifications;
using Pessoa.Fisica.Appliation.Commands;
using Pessoa.Fisica.Domain.Entity;
using Pessoa.Fisica.Infraestructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Pessoa.Fisica.Appliation.CommandHandles
{
    public class PessoaFisicaCommandHandler :
        IRequestHandler<CadastrarPessoaFisicaCommand, bool>,
        IRequestHandler<EditarPessoaFisicaCommand, bool>,
         IRequestHandler<InativarPessoaFisicaCommand, bool>
    {
        private readonly IMediatorHandler _mediateHandler;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<PessoaFisica, PessoaFisicaContext> _unitOfWorkPessoaFisica;
        public PessoaFisicaCommandHandler(IMediatorHandler mediateHandler,
            IMapper mapper,
            IUnitOfWork<PessoaFisica, PessoaFisicaContext> unitOfWorkPessoaFisica)
        {
            _mediateHandler = mediateHandler;
            _mapper = mapper;
            _unitOfWorkPessoaFisica = unitOfWorkPessoaFisica;
        }

        public async Task<bool> Handle(CadastrarPessoaFisicaCommand request, CancellationToken cancellationToken)
        {
            if (!ValidarCommand(request)) return false;

            var pessoaFisica = _mapper.Map<PessoaFisica>(request);
            await _unitOfWorkPessoaFisica.Repository.Add(pessoaFisica);
            return await _unitOfWorkPessoaFisica.Commit();
        }

        public async Task<bool> Handle(EditarPessoaFisicaCommand request, CancellationToken cancellationToken)
        {
            if (!ValidarCommand(request)) return false;

            var pessoaFisicaExistente = _unitOfWorkPessoaFisica.Repository.Where(pf => pf.Id == request.Id).Any();

            if (!pessoaFisicaExistente) throw new DomainException("Pessoa Física não encontrada");
            var pessoaFisica = _mapper.Map<PessoaFisica>(request);
            _unitOfWorkPessoaFisica.Repository.Update(pessoaFisica);

            return await _unitOfWorkPessoaFisica.Commit();
        }

        public async Task<bool> Handle(InativarPessoaFisicaCommand request, CancellationToken cancellationToken)
        {
            if (!ValidarCommand(request)) return false;
           
            var pessoaFisicaExistente = _unitOfWorkPessoaFisica.Repository.Where(pf => pf.Id == request.Id).SingleOrDefault();

            if (pessoaFisicaExistente is null) throw new DomainException("Pessoa Física não encontrada");
            pessoaFisicaExistente.Inativar();
            _unitOfWorkPessoaFisica.Repository.Update(pessoaFisicaExistente);
            return await _unitOfWorkPessoaFisica.Commit();
        }

        private bool ValidarCommand(Command command)
        {
            if (command.EhValido()) return true;

            foreach (var item in command.ValidationResult.Errors)
            {
                _mediateHandler.SendNotification(new DomainNotification(command.MessageType, item.ErrorMessage));
            }

            return false;
        }

        public void Dispose()
        {
            _unitOfWorkPessoaFisica?.Dispose();
        }
    }
}
