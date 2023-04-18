using AniMedia.Domain.Abstracts;
using AniMedia.Domain.Models.Responses;
using Microsoft.EntityFrameworkCore;

namespace AniMedia.Application;

public static class ResultExtensions {

    public static async Task<PagedResult<TValue>> CreatePagedResultAsync<TValue>(IQueryable<TValue> source, int page, int pageSize) {
        if (source == null) {
            throw new ArgumentNullException(nameof(source));
        }

        var countPages = (int)((double)await source.CountAsync() / pageSize);
        var items = await source
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PagedResult<TValue>(page, pageSize, countPages, items);
    }
}