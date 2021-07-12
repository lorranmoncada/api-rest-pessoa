using AutoMapper;
using Pessoa.Fisica.Appliation.Commands;
using Pessoa.Fisica.Appliation.Dto;
using Pessoa.Fisica.Domain.Entity;

namespace Pessoa.Fisica.Appliation.AutoMapper
{
    public class PessoaFisicaMap : Profile
    {
        public PessoaFisicaMap()
        {
            DtoToComand();
            CommandToDomainModel();
        }
        private void DtoToComand()
        {
            CreateMap<CadastroPessoaFisicaDto, CadastrarPessoaFisicaCommand>()
                .ConstructUsing(vw =>
                new CadastrarPessoaFisicaCommand(vw.Nome, vw.Cpf, vw.Endereco, vw.Contato, 'S'));

            CreateMap<EditarPessoaFisicaDto, EditarPessoaFisicaCommand>()
               .ConstructUsing(vw =>
               new EditarPessoaFisicaCommand(vw.Id, vw.Nome, vw.Cpf, vw.Endereco, vw.Contato, vw.Ativo ? 'S' : 'N')).IgnoreAllPropertiesWithAnInaccessibleSetter();
        }

        private void CommandToDomainModel()
        {
            CreateMap<CadastrarPessoaFisicaCommand, PessoaFisica>()
                .ConstructUsing(cm =>
                new PessoaFisica(cm.Nome, cm.Cpf, cm.Endereco, cm.Contato, cm.Ativo));

            CreateMap<EditarPessoaFisicaCommand, PessoaFisica>()
                .ConstructUsing(cm =>
                new PessoaFisica(cm.Nome, cm.Cpf, cm.Endereco, cm.Contato, cm.Ativo));
        }
    }
}
