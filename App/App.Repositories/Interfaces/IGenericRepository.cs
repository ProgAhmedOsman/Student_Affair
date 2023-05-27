
using APP.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace App.Repositories
{
    public interface IGenericRepository<T> where T : EntityBase
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetAsync(dynamic id);
        Task InsertAsync(T entity);
        Task UpdateAsync(dynamic id, T entity);
        Task DeleteAsync(T entity);
        Task RemoveAsync(T entity);
        Task SaveChangesAsync();
    }

}
