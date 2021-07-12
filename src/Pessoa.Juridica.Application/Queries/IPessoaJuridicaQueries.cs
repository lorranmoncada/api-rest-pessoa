using Pessoa.Juridica.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pessoa.Juridica.Appliation.Queries
{
    public interface IPessoaJuridicaQueries
    {
        Task<IEnumerable<PessoaJuridica>> PessoasJuridicas();
        Task<PessoaJuridica> PessoaJuridica(Guid id);
    }
}
