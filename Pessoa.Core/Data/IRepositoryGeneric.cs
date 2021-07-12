using Microsoft.EntityFrameworkCore;
using Pessoa.Core.DomainObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Pessoa.Core.Data
{
    public interface IRepositoryGeneric<TEntity, Context> : IDisposable where TEntity : BaseEntity where Context : DbContext
    {
        Task Add(TEntity entity);

        Task Add(IEnumerable<TEntity> items);

        void Update(TEntity entity);

        void Update(IEnumerable<TEntity> items);

        void Delete(TEntity entity);

        void Delete(IEnumerable<TEntity> entities);

        Task<IEnumerable<TEntity>> All();

        Task<TEntity> GetById(Guid id);

        IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> expression);
    }
}
