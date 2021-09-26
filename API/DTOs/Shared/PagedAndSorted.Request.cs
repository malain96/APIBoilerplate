namespace API.Shared.DTOs
{
    /// <summary>
    /// Input for requests that needs pagination and sorting
    /// </summary>
    public class PagedAndSortedRequest
    {
        private const int MaxPageSize = 50;

        private int _pageSize = 10;

        private string _sort = "asc";

        /// <summary>
        /// Page number
        /// </summary>
        public int PageNumber { get; set; } = 1;

        /// <summary>
        /// Number of element per page
        /// </summary>
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = value > MaxPageSize ? MaxPageSize : value;
        }

        /// <summary>
        /// Sorting direction (asc or desc)
        /// </summary>
        public string Sort
        {
            get => _sort;
            set => _sort = value.ToLowerInvariant() == "desc" ? value : _sort;
        }

        /// <summary>
        /// Property to sort by
        /// </summary>
        public string Sortby { get; set; }

        /// <summary>
        /// Search string
        /// </summary>
        public string Search { get; set; }
    }
}