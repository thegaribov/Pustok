using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Text.Json;
using System;

namespace Pustok.Helpers.Paging;

public class Paginator<TEntity> where TEntity : class, new()
{
    private const int DefaultPage = 1;
    private const int DefaultPageSize = 10;

    [JsonIgnore]
    public IQueryable<TEntity> QuerySet { get; set; }

    [JsonIgnore]
    public IEnumerable<TEntity> Records { get; set; }

    public int PageSize { get; private set; }

    public int? PreviousPage { get; private set; }
    public int CurrentPage { get; private set; }
    public int? NextPage { get; private set; }
    public int LastPage { get; private set; }

    public int TotalRecords { get; private set; }


    public Paginator(IQueryable<TEntity> query, int? page, int? pageSize)
    {
        PageSize = pageSize ?? DefaultPageSize;
        TotalRecords = query.Count();

        LastPage = GetLastPage(TotalRecords, PageSize);
        CurrentPage = GetCurrentPage(page ?? DefaultPage, LastPage);

        NextPage = GetNextPage(CurrentPage, HasNextPage(CurrentPage, LastPage));
        PreviousPage = GetPreviousPage(CurrentPage, HasPreviousPage(CurrentPage));

        var skipCount = CalculateSkipCount(CurrentPage, PageSize);
        QuerySet = query.Skip(skipCount).Take(PageSize);
    }

    private int GetCurrentPage(int requestedPage, int lastPage)
    {
        return requestedPage > lastPage ? lastPage : requestedPage;
    }

    private int GetLastPage(int recordsCount, int pageSize)
    {
        if (recordsCount <= 0) return DefaultPage;

        var pageCount = (double)recordsCount / pageSize;
        return (int)Math.Ceiling(pageCount);
    }

    private bool HasNextPage(int currentPage, int lastPage)
        => lastPage - currentPage > 0;

    private int? GetNextPage(int currentPage, bool hasNextPage)
        => hasNextPage ? currentPage + 1 : null;

    private bool HasPreviousPage(int currentPage)
    {
        int firstPage = 1;
        return currentPage - firstPage > 0;
    }

    private int? GetPreviousPage(int currentPage, bool hasPreviousPage)
        => hasPreviousPage ? currentPage - 1 : null;

    private int CalculateSkipCount(int currentPage, int pageSize)
        => (currentPage - 1) * pageSize;

    /// <summary>
    /// Returns information about pagination
    /// </summary>
    public override string ToString()
        => JsonSerializer.Serialize(this, new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
}
