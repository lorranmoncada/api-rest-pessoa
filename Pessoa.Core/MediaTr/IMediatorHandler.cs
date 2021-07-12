using Pessoa.Core.Message;
using Pessoa.Core.Message.CommonMessages.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pessoa.Core.MediaTr
{
    public interface IMediatorHandler
    {
        Task SendCommand<T>(T command) where T : Command;

        Task SendNotification<T>(T notification) where T : DomainNotification;
    }
}
