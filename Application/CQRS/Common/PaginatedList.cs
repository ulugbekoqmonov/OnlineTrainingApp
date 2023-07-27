namespace Application.CQRS.Common;

public class PaginatedList<T>
{
    public int PageNumber { get; private set; }
    public int TotalPages { get; private set; }    
    public IEnumerable<T> Items { get; private set; }
    public bool HasPreviousPage => PageNumber > 1;
    public bool HasNextPage => PageNumber < TotalPages;

    public PaginatedList(IEnumerable<T> items, int pageNumber = 1, int pageSize = 10)
    {
        PageNumber = pageNumber;
        TotalPages = (int)Math.Ceiling(items.Count() / (decimal)pageSize);
        Items = items.Skip((pageNumber - 1) * pageSize).Take(pageSize);
    }
}
