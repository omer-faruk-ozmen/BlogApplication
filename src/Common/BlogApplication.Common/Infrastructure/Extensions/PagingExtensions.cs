using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogApplication.Common.Models.Page;
using Microsoft.EntityFrameworkCore;

namespace BlogApplication.Common.Infrastructure.Extensions
{
    public static class PagingExtensions
    {
        public static async Task<PageViewModel<T>> GetPaged<T>(this IQueryable<T> query, int currentPage, int pageSize) where T : class
        {
            var count = await query.CountAsync();

            Page paging = new(currentPage = 1, pageSize = 10, count);

            var data = await query
                .Skip(paging.Skip)
                .Take(paging.PageSize)
                .AsNoTracking()
                .ToListAsync();

            var result = new PageViewModel<T>(data, paging);

            return result;
        }
    }
}
