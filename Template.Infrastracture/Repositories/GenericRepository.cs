using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReportsBackend.Domain.Helpers;
using ReportsBackend.Domain.Interfaces;
using ReportsBackend.Infrastracture.Data.Context;
using ReportsBackend.Infrastracture.Interfaces;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

namespace ReportsBackend.Infrastracture.Repositories
{

    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly ApplicationDbContext _context;
        protected readonly DbSet<T> _dbSet;
        private readonly ICurrentUserService _currentUserService;
        private readonly ILogger<GenericRepository<T>> _logger;

        public GenericRepository(ApplicationDbContext context,
            ICurrentUserService currentUserService,
            ILogger<GenericRepository<T>> logger
            )
        {
            _context = context;
            _dbSet = context.Set<T>();
            _currentUserService = currentUserService;
            _logger = logger;
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
            //await _dbSet.AddAsync(entity);
            //await _context.SaveChangesAsync();
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity));

                if (entity is IAuditableEntity auditableEntity)
                {
                    auditableEntity.CreatedAt = DateTime.Now;
                    auditableEntity.CreatedBy = GetCurrentUserId(); // Set CreatedBy to current user
                }
                await _dbSet.AddAsync(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding entity of type {Type}", typeof(T).Name);
                throw;
            }
        }

        public async Task Update(T entity)
        {
            //_context.Set<T>().Update(entity);
            //await _context.SaveChangesAsync();

            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity));

                if (entity is IAuditableEntity auditableEntity)
                {
                    auditableEntity.UpdatedAt = DateTime.Now;
                    auditableEntity.UpdatedBy = GetCurrentUserId(); // Set UpdatedBy to current user
                }

                _dbSet.Update(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating entity of type {Type}", typeof(T).Name);
                throw;
            }

        }

        public async Task Delete(T entity)
        {
            //_dbSet.Remove(entity);
            //await _context.SaveChangesAsync();
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity));

                if (entity is ISoftDeletable softDeletable)
                {
                    softDeletable.IsDeleted = true;
                    softDeletable.DeletedAt = DateTime.Now;
                    softDeletable.DeletedBy = GetCurrentUserId(); // Set DeletedBy to current user
                    _dbSet.Update(entity);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    _dbSet.Remove(entity);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting entity of type {Type}", typeof(T).Name);
                throw;
            }
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

        public async Task<IEnumerable<T>> FindAsync(
            Expression<Func<T, bool>> predicate,
            params Func<IQueryable<T>, IQueryable<T>>[] includes)
        {
            try
            {
                IQueryable<T> query = _dbSet;

                if (includes != null)
                {
                    foreach (var include in includes)
                    {
                        query = include(query);
                    }
                }

                return await query.Where(predicate).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while finding entities of type {Type}", typeof(T).Name);
                throw;
            }
        }
        private string GetCurrentUserId() => _currentUserService.Username ?? "system";

    }
}
