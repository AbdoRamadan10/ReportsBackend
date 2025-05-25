using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReportsBackend.Domain.Helpers;

namespace ReportsBackend.Domain.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<PaginatedResult<T>> GetAllAsync(FindOptions options, params Func<IQueryable<T>, IQueryable<T>>[] includes);
        Task<T> GetByIdAsync(int id, params Func<IQueryable<T>, IQueryable<T>>[] includes);
        Task AddAsync(T entity);
        Task Update(T entity);
        Task Delete(T entity);
    }
}
