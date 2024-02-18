using System.Collections.Generic;
using System.Linq;

namespace BankA.Application.Models
{
    public static class QueryableExtensions
    {
        public static PagedListResponse<T> PagedList<T>(this IQueryable<T> source, int pageIndex, int pageSize)
        {
            var count = source.Count();
            var items = source.Skip((pageIndex) * pageSize).Take(pageSize).ToList();
            return new PagedListResponse<T>(items, count, pageIndex, pageSize);
        }

        public static PagedListResponse<T> PagedList<T>(this IEnumerable<T> source, int pageIndex, int pageSize)
        {
            var count = source.Count();
            var items = source.Skip((pageIndex) * pageSize).Take(pageSize).ToList();
            return new PagedListResponse<T>(items, count, pageIndex, pageSize);
        }
    }
}
