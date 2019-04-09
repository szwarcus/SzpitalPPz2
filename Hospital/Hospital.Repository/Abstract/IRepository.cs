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
        Task<List<TResult>> GetAllAsync<TResult>(Expression<Func<TEntity, TResult>> select,
                                                 Expression<Func<TEntity, bool>> filter = null,
                                                 Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                                 Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includes = null)
                                                 where TResult : class;

        Task<List<TResult>> GetAsync<TResult>(Expression<Func<TEntity, TResult>> select, 
                                              Expression<Func<TEntity, bool>> filter = null,
                                              Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                              Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includes = null,
                                              int skip = 0,
                                              int take = 1)
                                              where TResult : class;

        Task<int> CountAllAsync();
        Task InsertAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(TEntity entity);
    }
}