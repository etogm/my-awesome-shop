using Microsoft.EntityFrameworkCore;

namespace MyAwesomeShop.Shared.Application;

public class PaginatedList<T> where T : class
{
    public PaginatedList(IReadOnlyCollection<T> items, int count, int currentPage, int perPage)
    {
        CurrentPage = currentPage;
        TotalPages = (int)Math.Ceiling(count / (double)perPage);
        TotalCount = count;
        Items = items;
    }

    public IReadOnlyCollection<T> Items { get; }

    public int CurrentPage { get; }

    public int TotalPages { get; }

    public int TotalCount { get; }

    public static async Task<PaginatedList<T>> CreateAsync(IQueryable<T> source, int currentPage, int perPage)
    {
        var count = await source.CountAsync();
        var items = await source.AsNoTracking().Skip((currentPage - 1) * perPage).Take(perPage).ToListAsync();

        return new PaginatedList<T>(items, count, currentPage, perPage);
    }
}
