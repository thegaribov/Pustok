namespace Pustok.Helpers.Paging;

public class QueryParams
{
    public QueryParams() { }

    public QueryParams(string search, string sort, int? page, int? pageSize)
    {
        Search = search;
        Sort = sort;
        Page = page;
        PageSize = pageSize;
    }


    /// <summary>
    /// Search query
    /// </summary>
    public string Search { get; set; }

    /// <summary>
    /// Sort query
    /// </summary>
    public string Sort { get; set; }

    /// <summary>
    /// Page number to request
    /// </summary>
    public int? Page { get; set; }

    /// <summary>
    /// Page size to request
    /// </summary>
    public int? PageSize { get; set; }
}
