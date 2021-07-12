using AutoMapper;
using MediatR;
using Pessoa.Core.Data;
using Pessoa.Core.DomainObject;
using Pessoa.Core.MediaTr;
using Pessoa.Core.Message;
using Pessoa.Core.Message.CommonMessages.Notifications;
using Pessoa.Juridica.Appliation.Commands;
using Pessoa.Juridica.Domain.Entity;
using Pessoa.Juridica.Infraestructure;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Pessoa.Jurdica.Appliation.CommandHandles
{
    public class PessoaJuridicaCommandHandler :
        IRequestHandler<CadastrarPessoaJuridicaCommand, bool>,
        IRequestHandler<EditarPessoaJuridicaCommand, bool>,
         IRequestHandler<InativarPessoaJuridicaCommand, bool>
    {
        private readonly IMediatorHandler _mediateHandler;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<PessoaJuridica, PessoaJuridicaContext> _unitOfWorkPessoaJuridica;
        public PessoaJuridicaCommandHandler(IMediatorHandler mediateHandler,
            IMapper mapper,
            IUnitOfWork<PessoaJuridica, PessoaJuridicaContext> unitOfWorkPessoaJuridica)
        {
            _mediateHandler = mediateHandler;
            _mapper = mapper;
            _unitOfWorkPessoaJuridica = unitOfWorkPessoaJuridica;
        }

        public async Task<bool> Handle(CadastrarPessoaJuridicaCommand request, CancellationToken cancellationToken)
        {
            if (!ValidarCommand(request)) return false;

            var PessoaJuridica = _mapper.Map<PessoaJuridica>(request);
            await _unitOfWorkPessoaJuridica.Repository.Add(PessoaJuridica);
            return await _unitOfWorkPessoaJuridica.Commit();
        }

        public async Task<bool> Handle(EditarPessoaJuridicaCommand request, CancellationToken cancellationToken)
        {
            if (!ValidarCommand(request)) return false;

            var PessoaJuridicaExistente = _unitOfWorkPessoaJuridica.Repository.Where(pf => pf.Id == request.Id).Any();

            if (!PessoaJuridicaExistente) throw new DomainException("Pessoa Jurídica não encontrada");
            var PessoaJuridica = _mapper.Map<PessoaJuridica>(request);
            _unitOfWorkPessoaJuridica.Repository.Update(PessoaJuridica);

            return await _unitOfWorkPessoaJuridica.Commit();
        }

        public async Task<bool> Handle(InativarPessoaJuridicaCommand request, CancellationToken cancellationToken)
        {
            if (!ValidarCommand(request)) return false;
           
            var PessoaJuridicaExistente = _unitOfWorkPessoaJuridica.Repository.Where(pf => pf.Id == request.Id).SingleOrDefault();

            if (PessoaJuridicaExistente is null) throw new DomainException("Pessoa Jurídica não encontrada");
            PessoaJuridicaExistente.Inativar();
            _unitOfWorkPessoaJuridica.Repository.Update(PessoaJuridicaExistente);

            return await _unitOfWorkPessoaJuridica.Commit();
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
            _unitOfWorkPessoaJuridica?.Dispose();
        }
    }
}
