using Microsoft.EntityFrameworkCore;
using Pessoa.Core.Data;
using Pessoa.Core.DomainObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Pessoa.Juridica.Infraestructure
{
    public class RepositoryGeneric<TEntity, Context> : IRepositoryGeneric<TEntity, Context>
        where TEntity : BaseEntity
        where Context : DbContext
    {
        DbSet<TEntity> Entity { get; set; }
        private readonly Context _pessoaFisicaContext;
        public RepositoryGeneric(Context pessoaFisicaContext)
        {
            Entity = pessoaFisicaContext.Set<TEntity>();
            _pessoaFisicaContext = pessoaFisicaContext;
        }

        public async Task Add(TEntity entity)
        {
            await Entity.AddAsync(entity);
        }

        public async Task Add(IEnumerable<TEntity> items)
        {
            await Entity.AddRangeAsync(items);
        }

        public async Task<IEnumerable<TEntity>> All()
        {
            return await Entity.AsNoTracking().ToListAsync();
        }

        public void Delete(TEntity entity)
        {
            Entity.Remove(entity);
        }

        public void Delete(IEnumerable<TEntity> entities)
        {
            throw new NotImplementedException();
        }

        public async Task<TEntity> GetById(Guid id)
        {
            return await Entity.SingleAsync(x => x.Id == id);
        }

        public void Update(TEntity entity)
        {
            Entity.Update(entity);
        }

        public void Update(IEnumerable<TEntity> items)
        {
            throw new NotImplementedException();
        }
 
        public void Dispose()
        {
            _pessoaFisicaContext?.Dispose();
        }

        public IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> expression)
        {
            return Entity.Where(expression);
        }   
    }
}
