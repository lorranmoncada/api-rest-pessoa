using Pessoa.Fisica.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pessoa.Fisica.Appliation.Queries
{
    public interface IPessoaFisicaQueries
    {
        Task<IEnumerable<PessoaFisica>> PessoasFisicas();
        Task<PessoaFisica> PessoaFisica(Guid id);
    }
}
