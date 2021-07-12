using Microsoft.EntityFrameworkCore;
using Pessoa.Core.DomainObject;
using System;
using System.Threading.Tasks;

namespace Pessoa.Core.Data
{
    public interface IUnitOfWork<TEntity, Context>: IDisposable where TEntity : BaseEntity where Context : DbContext
    {
        IRepositoryGeneric<TEntity, Context> Repository { get; }
        Task<bool> Commit();
    }
}
