using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReportsBackend.Domain.Helpers;
using ReportsBackend.Domain.Interfaces;
using ReportsBackend.Infrastracture.Data.Context;

namespace ReportsBackend.Infrastracture.Repositories
{

    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly ApplicationDbContext _context;
        protected readonly DbSet<T> _dbSet;

        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task<PaginatedResult<T>> GetAllAsync(FindOptions options, params Func<IQueryable<T>, IQueryable<T>>[] includes)
        {
            IQueryable<T> query = _dbSet;

            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = include(query);
                }
            }

            return await query.GetPagedAsync(options);
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Update(T entity)
        {
            //_dbSet.Attach(entity);
            //_context.Entry(entity).State = EntityState.Modified;
            //await _context.SaveChangesAsync();
            //if (entity is BaseEntity baseEntity)
            //{
            //    baseEntity.UpdatedAt = DateTime.Now; // Set the timestamp
            //}

            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();

        }

        public async Task Delete(T entity)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }



        public Task<T> GetByIdAsync(int id, params Func<IQueryable<T>, IQueryable<T>>[] includes)
        {
            IQueryable<T> query = _dbSet;
            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = include(query);
                }
            }
            return query.FirstOrDefaultAsync(e => EF.Property<int>(e, "Id") == id);

        }
    }
}
