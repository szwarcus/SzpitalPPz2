using System.Collections.Generic;
using System.Threading.Tasks;
using Hospital.Model.Entities;

namespace Hospital.Repository.Abstract
{
    public interface IRepository<T> where T : BaseEntity
    {
        IEnumerable<T> GetAll();
        Task<T> Get(long id);
        Task Insert(T entity);
        Task Update(T entity);
        Task Delete(T entity);
    }
}