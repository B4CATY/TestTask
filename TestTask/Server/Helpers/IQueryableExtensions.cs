using System.Linq;
using TestTask.Shared.Pagination;

namespace TestTask.Server.Helpers
{
    public static class IQueryableExtensions
    {
        public static IQueryable<T> Paginate<T>(this IQueryable<T> queryable, 
            PaginationDTO pagination)
        {
            return queryable.Skip((pagination.Page - 1) * pagination.PageSize)
                .Take(pagination.PageSize);
        }
    }
}
