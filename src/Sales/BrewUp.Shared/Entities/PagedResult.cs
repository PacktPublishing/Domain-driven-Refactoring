namespace BrewUp.Shared.Entities
{
    public class PagedResult<T>
    {
        public PagedResult(IEnumerable<T> results, int page, int pageSize, int totalRecords)
        {
            Results = results;
            Page = page;
            PageSize = pageSize;
            TotalRecords = totalRecords;
        }

        public IEnumerable<T> Results { get; }
        public int PageSize { get; }
        public int Page { get; }
        public int TotalRecords { get; }
    }
}