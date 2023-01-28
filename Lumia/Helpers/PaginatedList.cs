namespace Lumia.Helpers
{
    public class PaginatedList<T> : List<T>
    {
        public PaginatedList(List<T> values, int count, int pageSize, int page)
        {
            this.AddRange(values);
            TotalPageCount = (int)Math.Ceiling((double)count / pageSize);
            ActivePage = page;
        }

        public int TotalPageCount { get; set; }
        public int ActivePage { get; set; }
        public bool HasNext { get => ActivePage < TotalPageCount; }
        public bool HasPrevious { get => ActivePage > 1; }

        public static PaginatedList<T> Create(IQueryable<T> query, int pageSize, int page)
        {
            return new PaginatedList<T>( query.Skip( (page - 1) * pageSize ).Take(pageSize).ToList(), query.Count(), pageSize, page );
        }

    }
}