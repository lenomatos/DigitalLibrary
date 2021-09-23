using DigitalLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DigitalLibrary.Repository
{
    public interface IRepository<TEntity> : IDisposable where TEntity : Entity
    {
        Task<TEntity> GetById(Guid id);
        Task<TEntity> FirstOrDefault(Expression<Func<TEntity, bool>> predicate);

        Task Add(TEntity entity);
        Task Update(TEntity entity);
        Task Remove(Guid id);

        Task<int> SaveChanges();

        Task<List<TEntity>> GetAll();
        Task<IEnumerable<TEntity>> GetWhere(Expression<Func<TEntity, bool>> predicate);

        Task<int> CountAll();
        Task<int> CountWhere(Expression<Func<TEntity, bool>> predicate);
    }
}
