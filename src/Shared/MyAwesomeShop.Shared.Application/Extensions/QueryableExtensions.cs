using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore;

namespace MyAwesomeShop.Shared.Application.Extensions;

public static class QueryableExtensions
{
    public static async Task<PaginatedList<T>> ToPaginatedListAsync<T>(this IQueryable<T> source, int currentPage, int perPage) where T : class
    {
        var count = await source.CountAsync();
        var items = await source.Skip((currentPage - 1) * perPage).Take(perPage).ToListAsync();

        return new PaginatedList<T>(items, count, currentPage, perPage);
    }
}
