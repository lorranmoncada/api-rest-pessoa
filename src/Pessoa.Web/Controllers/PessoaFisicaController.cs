using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Pessoa.Core.MediaTr;
using Pessoa.Core.Message.CommonMessages.Notifications;
using Pessoa.Fisica.Appliation.Commands;
using Pessoa.Fisica.Appliation.Dto;
using Pessoa.Fisica.Appliation.Queries;
using Pessoa.Fisica.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pessoa.Web.Controllers
{
    /// <summary>
    /// Api de pessoa física
    /// </summary>
    [Produces("application/json")]
    [Route("api/v1/pessoa-fisica")]
    public class PessoaFisicaController : Pessoa.Web.Controllers.ControllerBase.ControllerBase
    {
        private readonly IMediatorHandler _mediateHandler;
        INotificationHandler<DomainNotification> _domainNotificationHandler;
        private readonly IMapper _mapper;
        private readonly IPessoaFisicaQueries _pessoaFisicaQuery;

        public PessoaFisicaController(
            IMediatorHandler mediateHandler,
            INotificationHandler<DomainNotification> domainNotificationHandler,
            IMapper mapper,
            IPessoaFisicaQueries pessoaFisicaQuery)
            : base(domainNotificationHandler, mediateHandler)
        {
            _mediateHandler = mediateHandler;
            _domainNotificationHandler = domainNotificationHandler;
            _mapper = mapper;
            _pessoaFisicaQuery = pessoaFisicaQuery;
        }

        /// <summary>
        /// Cadastra uma pessoa física
        /// </summary>
        [HttpPost]
        [Route("cadastrar")]
        public async Task<IActionResult> Cadastro([FromBody] CadastroPessoaFisicaDto pessoaFisica)
        {
            var cadastroCommand = _mapper.Map<CadastrarPessoaFisicaCommand>(pessoaFisica);
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
        /// Edita uma pessoa física
        /// </summary>
        [HttpPut]
        [Route("editar")]
        public async Task<IActionResult> Editar([FromBody] EditarPessoaFisicaDto pessoaFisica)
        {
            var editarCommand = _mapper.Map<EditarPessoaFisicaCommand>(pessoaFisica);
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
        /// Inativa uma pessoa física
        /// </summary>
        [HttpPut]
        [Route("inativar/{id:guid}")]
        public async Task<IActionResult> Inativar(Guid id)
        {
            var inativarCommand = new InativarPessoaFisicaCommand(id);
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
        /// Retorna todas as pessoas físicas ativas
        /// </summary>
        [HttpGet]
        [Route("ativas")]
        public async Task<IEnumerable<PessoaFisica>> PessoasFisicas()
        {
            return await _pessoaFisicaQuery.PessoasFisicas();
        }

        /// <summary>
        /// Retorna uma as pessoas físicas pelo id
        /// </summary>
        [HttpGet]
        [Route("{id:guid}")]
        public async Task<PessoaFisica> PessoasFisicas(Guid id)
        {
            return await _pessoaFisicaQuery.PessoaFisica(id);
        }
    }
}
