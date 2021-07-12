

using AutoMapper;
using Pessoa.Juridica.Appliation.Commands;
using Pessoa.Juridica.Appliation.Dto;
using Pessoa.Juridica.Domain.Entity;

namespace Pessoa.Juridica.Appliation.AutoMapper
{
    public class PessoaJuridicaMap : Profile
    {
        public PessoaJuridicaMap()
        {
            DtoToComand();
            CommandToDomainModel();
        }
        private void DtoToComand()
        {
            CreateMap<CadastroPessoaJuridicaDto, CadastrarPessoaJuridicaCommand>()
                .ConstructUsing(vw =>
                new CadastrarPessoaJuridicaCommand(vw.RazaoSocial, vw.Cnpj, vw.Endereco, vw.Contato, 'S'));

            CreateMap<EditarPessoaJuridicaDto, EditarPessoaJuridicaCommand>()
               .ConstructUsing(vw =>
               new EditarPessoaJuridicaCommand(vw.Id, vw.RazaoSocial, vw.Cnpj, vw.Endereco, vw.Contato, vw.Ativo ? 'S' : 'N')).IgnoreAllPropertiesWithAnInaccessibleSetter();
        }

        private void CommandToDomainModel()
        {
            CreateMap<CadastrarPessoaJuridicaCommand, PessoaJuridica>()
                .ConstructUsing(cm =>
                new PessoaJuridica(cm.RazaoSocial, cm.Cnpj, cm.Endereco, cm.Contato, cm.Ativo));

            CreateMap<EditarPessoaJuridicaCommand, PessoaJuridica>()
                .ConstructUsing(cm =>
                new PessoaJuridica(cm.RazaoSocial, cm.Cnpj, cm.Endereco, cm.Contato, cm.Ativo));
        }
    }
}
