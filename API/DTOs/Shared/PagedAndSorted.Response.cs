using System.Collections.Generic;

namespace API.Shared.DTOs
{
    /// <summary>
    /// Reponse for requests that needs pagination and sorting
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PagedAndSortedResponse<T>
    {
        /// <summary>
        /// The list of items
        /// </summary>
        public List<T> Items { get; set; }

        /// <summary>
        /// Sorting direction (asc or desc)
        /// </summary>
        public string Sorted { get; set; }

        /// <summary>
        /// Property to sorted by
        /// </summary>
        public string SortedBy { get; set; }

        /// <summary>
        /// Search string
        /// </summary>
        public string Search { get; set; }

        /// <summary>
        /// Number of the current page
        /// </summary>
        public int CurrentPage { get; set; }

        /// <summary>
        /// Total number of pages
        /// </summary>
        public int TotalPages { get; set; }

        /// <summary>
        /// Number of element per page
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// Total number of elements
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// Has a previous page
        /// </summary>
        public bool HasPrevious { get; set; }

        /// <summary>
        /// Has a next page
        /// </summary>
        public bool HasNext { get; set; }
    }
}