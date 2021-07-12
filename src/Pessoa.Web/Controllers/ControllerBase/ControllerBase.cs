using MediatR;
using Microsoft.AspNetCore.Mvc;
using Pessoa.Core.MediaTr;
using Pessoa.Core.Message.CommonMessages.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Pessoa.Web.Controllers.ControllerBase
{
    public abstract class ControllerBase : Controller
    {
        private readonly DomainNotificationHandler _DomainNotificationHandler;
        private readonly IMediatorHandler _IMediatorHandler;
        protected Guid ClienteId = Guid.Parse("4885e451-b0e4-4490-b959-04fabc806d32");

        protected ControllerBase(INotificationHandler<DomainNotification> DomainNotificationHandler, IMediatorHandler IMediatorHandler)
        {
            _DomainNotificationHandler = (DomainNotificationHandler)DomainNotificationHandler;
            _IMediatorHandler = IMediatorHandler;
        }

        protected bool OperacaoInvalida()
        {
            return _DomainNotificationHandler.TemNotificacao();
        }

        protected IList<string> AllNotificationsValues()
        {
            return _DomainNotificationHandler.ObterNotificacoes().Select(n => n.Value).ToArray();
        }

        protected void NotificarErro(string codigo, string mensagem)
        {
            _IMediatorHandler.SendNotification(new DomainNotification(codigo, mensagem));
        }

        protected void LimparMensagens()
        {
            _DomainNotificationHandler.Dispose();
        }
    }
}
