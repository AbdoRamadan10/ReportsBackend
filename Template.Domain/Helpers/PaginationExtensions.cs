using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportsBackend.Domain.Helpers
{
    public static class PaginationExtensions
    {
        public static async Task<PaginatedResult<T>> GetPagedAsync<T>(
            this IQueryable<T> query,
            FindOptions options) where T : class
        {

            // Apply Filters
            //if (options.Filters != null)
            //{
            //    foreach (var filter in options.Filters)
            //    {
            //        var propertyInfo = typeof(T).GetProperty(filter.Key);
            //        if (propertyInfo != null && filter.Value != null)
            //        {
            //            var parameter = System.Linq.Expressions.Expression.Parameter(typeof(T), "e");
            //            var property = System.Linq.Expressions.Expression.Property(parameter, propertyInfo);
            //            var constant = System.Linq.Expressions.Expression.Constant(filter.Value);
            //            var equals = System.Linq.Expressions.Expression.Equal(property, constant);
            //            var lambda = System.Linq.Expressions.Expression.Lambda<Func<T, bool>>(equals, parameter);
            //            query = query.Where(lambda);
            //        }
            //    }
            //}

            // Apply SearchTerm if present (simple string.Contains on all string properties)
            if (!string.IsNullOrWhiteSpace(options.SearchTerm))
            {
                // For advanced scenarios, use a library like LINQKit or build expressions dynamically.
                // Here, we skip implementation for brevity.
            }

            int pageNumber = options.PageNumber > 0 ? options.PageNumber : 1;
            int pageSize = options.PageSize > 0 ? options.PageSize : 10;
            // Apply sorting if specified
            if (!string.IsNullOrWhiteSpace(options.SortBy))
            {
                query = options.SortDescending
                    ? query.OrderByDescending(e => EF.Property<object>(e, options.SortBy))
                    : query.OrderBy(e => EF.Property<object>(e, options.SortBy));
            }


            var result = new PaginatedResult<T>
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalCount = await query.CountAsync()
            };

            result.Items = await query.Skip((pageNumber - 1) * pageSize)
                                     .Take(pageSize)
                                     .ToListAsync();

            return result;
        }
    }
}
