using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Hospital.Model.Entities;
using Microsoft.EntityFrameworkCore.Query;

namespace Hospital.Repository.Abstract
{
    public interface IRepository<TEntity> where TEntity : BaseEntity
    {
        IEnumerable<TEntity> GetAllAsync();
        Task<List<TResult>> GetAsync<TResult>(Expression<Func<TEntity, bool>> filter,
                                              Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy,
                                              Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includes,
                                              Expression<Func<TEntity, TResult>> select,
                                              int skip = 0,
                                              int take = 1)
                                              where TResult : class;
        Task InsertAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(TEntity entity);
    }
}