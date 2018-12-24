using System;
using System.Linq;
using Microsoft.EntityFrameworkCore.Query;

namespace Hospital.Model.Extensions
{
    public interface IIncludable { }

    public interface IIncludable<out TEntity> : IIncludable { }

    public interface IIncludable<out TEntity, out TProperty> : IIncludable<TEntity> { }

    public class Includable<TEntity> : IIncludable<TEntity> where TEntity : class
    {
        public IQueryable<TEntity> Input { get; }

        public Includable(IQueryable<TEntity> queryable)
        {
            Input = queryable ?? throw new ArgumentNullException(nameof(queryable));
        }
    }

    public class Includable<TEntity, TProperty> : Includable<TEntity>,
                                                  IIncludable<TEntity, TProperty> where TEntity : class
    {
        public IIncludableQueryable<TEntity, TProperty> IncludableInput { get; }

        public Includable(IIncludableQueryable<TEntity,TProperty> queryable) : base(queryable)
        {
            IncludableInput = queryable;
        }
    }
}