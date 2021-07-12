

using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Pessoa.Core.Data;
using Pessoa.Core.MediaTr;
using Pessoa.Core.Message.CommonMessages.Notifications;
using Pessoa.Fisica.Appliation.AutoMapper;
using Pessoa.Fisica.Appliation.CommandHandles;
using Pessoa.Fisica.Appliation.Commands;
using Pessoa.Fisica.Appliation.Queries;
using Pessoa.Fisica.Domain.Entity;
using Pessoa.Fisica.Infraestructure;
using Pessoa.Jurdica.Appliation.CommandHandles;
using Pessoa.Juridica.Appliation.AutoMapper;
using Pessoa.Juridica.Appliation.Commands;
using Pessoa.Juridica.Appliation.Queries;
using Pessoa.Juridica.Domain.Entity;
using Pessoa.Juridica.Infraestructure;

namespace Pessoa.Web.Extensions
{

    public static class DependencyInjector
    {

        public static void RegisterService(this IServiceCollection service)
        {
            //Notification
            service.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();

            //Metiator
            service.AddScoped<IMediatorHandler, MediatorHandler>();
        }
        public static void RegisterServicePessoaFisica(this IServiceCollection service)
        {
            //Contexto
            service.AddDbContext<PessoaFisicaContext>();

            //Generic 
            service.AddScoped(typeof(IUnitOfWork<PessoaFisica, PessoaFisicaContext>), typeof(UnitOfWork<PessoaFisica, PessoaFisicaContext>));
            service.AddScoped(typeof(IRepositoryGeneric<PessoaFisica, PessoaFisicaContext>), typeof(RepositoryGeneric<PessoaFisica, PessoaFisicaContext>));

            //Commands
            service.AddScoped<IRequestHandler<CadastrarPessoaFisicaCommand, bool>, PessoaFisicaCommandHandler>();
            service.AddScoped<IRequestHandler<EditarPessoaFisicaCommand, bool>, PessoaFisicaCommandHandler>();
            service.AddScoped<IRequestHandler<InativarPessoaFisicaCommand, bool>, PessoaFisicaCommandHandler>();

            //Querys
            service.AddScoped<IPessoaFisicaQueries, PessoaFisicaQueries>();
        }
        public static void RegisterServicePessoaJuridica(this IServiceCollection service)
        {
            //Contexto
            service.AddDbContext<PessoaJuridicaContext>();

            //Generic 
            service.AddScoped(typeof(IUnitOfWork<PessoaJuridica, PessoaJuridicaContext>), typeof(UnitOfWork<PessoaJuridica, PessoaJuridicaContext>));
            service.AddScoped(typeof(IRepositoryGeneric<PessoaJuridica, PessoaJuridicaContext>), typeof(RepositoryGeneric<PessoaJuridica, PessoaJuridicaContext>));

            //Commands
            service.AddScoped<IRequestHandler<CadastrarPessoaJuridicaCommand, bool>, PessoaJuridicaCommandHandler>();
            service.AddScoped<IRequestHandler<EditarPessoaJuridicaCommand, bool>, PessoaJuridicaCommandHandler>();
            service.AddScoped<IRequestHandler<InativarPessoaJuridicaCommand, bool>, PessoaJuridicaCommandHandler>();

            //Querys
            service.AddScoped<IPessoaJuridicaQueries, PessoaJuridicaQueries>();
        }
        public static void RegisterPessoaFisicaAutoMap(this IServiceCollection service)
        {
            service.AddAutoMapper(typeof(PessoaFisicaMap));
        }
        public static void RegisterPessoaJuridicaAutoMap(this IServiceCollection service)
        {
            service.AddAutoMapper(typeof(PessoaJuridicaMap));
        }
    }
}
