using Microsoft.EntityFrameworkCore;
using Pessoa.Core.Data;
using Pessoa.Fisica.Domain.Entity;
using Pessoa.Fisica.Infraestructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pessoa.Fisica.Appliation.Queries
{
    public class PessoaFisicaQueries : IPessoaFisicaQueries
    {
        private readonly IRepositoryGeneric<PessoaFisica, PessoaFisicaContext> _repository;

        public PessoaFisicaQueries(IRepositoryGeneric<PessoaFisica, PessoaFisicaContext> repository)
        {
            _repository = repository;
        }

        public async Task<PessoaFisica> PessoaFisica(Guid id)
        {
            return await _repository.GetById(id);
        }

        public async Task<IEnumerable<PessoaFisica>> PessoasFisicas()
        {
            return await _repository.Where(pf => pf.Ativo == 'S').ToListAsync();
        }

        public void Dispose()
        {
            _repository?.Dispose();
        }
    }
}
