using Microsoft.EntityFrameworkCore;
using Pessoa.Core.Data;
using Pessoa.Core.DomainObject;
using System.Threading.Tasks;

namespace Pessoa.Infraestructure
{
    public class UnitOfWork<TEntity, Context> : IUnitOfWork<TEntity, Context> 
        where TEntity : BaseEntity
        where Context : DbContext
    {
        private readonly Context _pessoaFisicaContext;
        public UnitOfWork(Context pessoaFisicaContext)
        {
            _pessoaFisicaContext = pessoaFisicaContext;
            Repository = new RepositoryGeneric<TEntity, Context>(_pessoaFisicaContext);
        }

        public IRepositoryGeneric<TEntity, Context> Repository { get; }

        public async Task<bool> Commit()
        {
           return await _pessoaFisicaContext.SaveChangesAsync() > 0;
        }

        public void Dispose()
        {
            _pessoaFisicaContext?.Dispose();
        }
    }
}
