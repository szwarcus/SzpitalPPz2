using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Hospital.Model.Entities;
using Hospital.Repository.Abstract;

namespace Hospital.Repository.Concrete
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly ApplicationDbContext _context;
        private DbSet<TEntity> _entities;
        string errorMessage = string.Empty;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
            _entities = context.Set<TEntity>();
        }

        public async Task<List<TResult>> GetAllAsync<TResult>(Expression<Func<TEntity, TResult>> select,
                                                              Expression<Func<TEntity, bool>> filter = null,
                                                              Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                                              Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includes = null)
                                                              where TResult : class
        {
            return await QueryDb(filter, orderBy, includes).Select(select).ToListAsync();
        }


        public async Task<List<TResult>> GetAsync<TResult>(Expression<Func<TEntity, TResult>> select, 
                                                           Expression<Func<TEntity, bool>> filter = null,
                                                           Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                                           Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includes = null,
                                                           int skip = 0,
                                                           int take = 1) 
                                                           where TResult : class
        {
            return await QueryDb(filter, orderBy, includes).Skip(skip)
                                                           .Take(take)
                                                           .Select(select)
                                                           .ToListAsync();
        }

        public async Task InsertAsync(TEntity entity)
        {
            if (IsEntityNull(entity)) return;

            _entities.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(TEntity entity)
        {
            if (IsEntityNull(entity)) return;

            await _context.SaveChangesAsync();
        }

        public async Task<int> CountAllAsync()
        {
            return await QueryDb().CountAsync();
        }

        public async Task DeleteAsync(TEntity entity)
        {
            if (IsEntityNull(entity)) return;

            _entities.Remove(entity);

            await _context.SaveChangesAsync();
        }

        protected IQueryable<TEntity> QueryDb(Expression<Func<TEntity, bool>> filter = null, 
                                              Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                              Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includes = null)
        {
            IQueryable<TEntity> query = _entities.AsQueryable();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includes != null)
            {
                query = includes(query);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            return query;
        }
        
        protected bool IsEntityNull(TEntity entity)
        {
            var result = entity == null;

            if (result) Console.WriteLine($"Entity {nameof(entity)} is null");

            return result;
        }
    }
}