using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Pessoa.Core.MediaTr;
using Pessoa.Core.Message.CommonMessages.Notifications;
using Pessoa.Juridica.Appliation.Commands;
using Pessoa.Juridica.Appliation.Dto;
using Pessoa.Juridica.Appliation.Queries;
using Pessoa.Juridica.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pessoa.Web.Controllers
{
    /// <summary>
    /// Api de pessoa jurídica
    /// </summary>
    [Produces("application/json")]
    [Route("api/v1/pessoa-juridica")]
    public class PessoaJuridicaController : Pessoa.Web.Controllers.ControllerBase.ControllerBase
    {
        private readonly IMediatorHandler _mediateHandler;
        INotificationHandler<DomainNotification> _domainNotificationHandler;
        private readonly IMapper _mapper;
        private readonly IPessoaJuridicaQueries _pessoaJuridicaQuery;

        public PessoaJuridicaController(
            IMediatorHandler mediateHandler,
            INotificationHandler<DomainNotification> domainNotificationHandler,
            IMapper mapper,
            IPessoaJuridicaQueries pessoaJuridicaQuery)
            : base(domainNotificationHandler, mediateHandler)
        {
            _mediateHandler = mediateHandler;
            _domainNotificationHandler = domainNotificationHandler;
            _mapper = mapper;
            _pessoaJuridicaQuery = pessoaJuridicaQuery;
        }

        /// <summary>
        /// Cadastra uma pessoa jurídica
        /// </summary>
        [HttpPost]
        [Route("cadastrar")]
        public async Task<IActionResult> Cadastro([FromBody] CadastroPessoaJuridicaDto pessoaJuridica)
        {
            var cadastroCommand = _mapper.Map<CadastrarPessoaJuridicaCommand>(pessoaJuridica);
            await _mediateHandler.SendCommand(cadastroCommand);
            if (OperacaoInvalida())
            {
                var erros = AllNotificationsValues();
                LimparMensagens();
                return BadRequest(erros);
            }          
            return Ok();
        }

        /// <summary>
        /// Edita uma pessoa jurídica
        /// </summary>
        [HttpPut]
        [Route("editar")]
        public async Task<IActionResult> Editar([FromBody] EditarPessoaJuridicaDto pessoaJuridica)
        {
            var editarCommand = _mapper.Map<EditarPessoaJuridicaCommand>(pessoaJuridica);
            await _mediateHandler.SendCommand(editarCommand);
            if (OperacaoInvalida())
            {
                var erros = AllNotificationsValues();
                LimparMensagens();
                return BadRequest(erros);
            }
            return Ok();
        }

        /// <summary>
        /// Inativa uma pessoa jurídica
        /// </summary>
        [HttpPut]
        [Route("inativar/{id:guid}")]
        public async Task<IActionResult> Inativar(Guid id)
        {
            var inativarCommand = new InativarPessoaJuridicaCommand(id);
            await _mediateHandler.SendCommand(inativarCommand);
            if (OperacaoInvalida())
            {
                var erros = AllNotificationsValues();
                LimparMensagens();
                return BadRequest(erros);
            }
            return Ok();
        }

        /// <summary>
        /// Retorna todas as pessoas jurídicas ativas
        /// </summary>
        [HttpGet]
        [Route("ativas")]
        public async Task<IEnumerable<PessoaJuridica>> PessoasJuridicas()
        {
            return await _pessoaJuridicaQuery.PessoasJuridicas();
        }

        /// <summary>
        /// Retorna uma as pessoa jurídica pelo id
        /// </summary>
        [HttpGet]
        [Route("{id:guid}")]
        public async Task<PessoaJuridica> PessoaJuridica(Guid id)
        {
            return await _pessoaJuridicaQuery.PessoaJuridica(id);
        }
    }
}
