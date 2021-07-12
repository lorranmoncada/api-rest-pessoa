using Microsoft.EntityFrameworkCore;
using Pessoa.Core.Data;
using Pessoa.Juridica.Domain.Entity;
using Pessoa.Juridica.Infraestructure;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pessoa.Juridica.Appliation.Queries
{
    public class PessoaJuridicaQueries : IPessoaJuridicaQueries
    {
        private readonly IRepositoryGeneric<PessoaJuridica, PessoaJuridicaContext> _repository;

        public PessoaJuridicaQueries(IRepositoryGeneric<PessoaJuridica, PessoaJuridicaContext> repository)
        {
            _repository = repository;
        }

        public async Task<PessoaJuridica> PessoaJuridica(Guid id)
        {
            return await _repository.GetById(id);
        }

        public async Task<IEnumerable<PessoaJuridica>> PessoasJuridicas()
        {
            return await _repository.Where(pf => pf.Ativo == 'S').ToListAsync();
        }

        public void Dispose()
        {
            _repository?.Dispose();
        }
    }
}
