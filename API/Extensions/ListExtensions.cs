using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Extensions
{
    /// <summary>
    /// Provides a paged and sorted list of T
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PagedAndSortedList<T> : List<T>
    {
        /// <summary>
        /// PagedAndSortedList Constructor
        /// </summary>
        /// <param name="items"></param>
        /// <param name="count"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="search"></param>
        /// <param name="sorted"></param>
        /// <param name="sortedBy"></param>
        public PagedAndSortedList(List<T> items, int count, int pageNumber, int pageSize, string search, string sorted,
            string sortedBy)
        {
            TotalCount = count;
            PageSize = pageSize;
            Sorted = sorted;
            SortedBy = sortedBy;
            Search = search;
            CurrentPage = pageNumber;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            Items = items;
        }

        /// <summary>
        /// Number of the current page
        /// </summary>
        public int CurrentPage { get; }
        /// <summary>
        /// Total number of pages
        /// </summary>
        public int TotalPages { get; }
        /// <summary>
        /// Number of element per page
        /// </summary>
        public int PageSize { get; }
        /// <summary>
        /// Total number of elements
        /// </summary>
        public int TotalCount { get; }
        /// <summary>
        /// Sorting direction (asc or desc)
        /// </summary>
        public string Sorted { get; }
        /// <summary>
        /// Property to sorted by
        /// </summary>
        public string SortedBy { get; }
        /// <summary>
        /// Search string
        /// </summary>
        public string Search { get; }
        /// <summary>
        /// The list of items
        /// </summary>
        public List<T> Items { get; }
        /// <summary>
        /// Has a previous page
        /// </summary>
        public bool HasPrevious => CurrentPage > 1;
        /// <summary>
        /// Has a next page
        /// </summary>
        public bool HasNext => CurrentPage < TotalPages;

        /// <summary>
        /// Convert an IQueryable to a PagedAndSortedList
        /// </summary>
        /// <param name="source"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="search"></param>
        /// <param name="sorted"></param>
        /// <param name="sortedBy"></param>
        /// <returns></returns>
        public static async Task<PagedAndSortedList<T>> ToPagedListAsync(IQueryable<T> source, int pageNumber,
            int pageSize, string search, string sorted, string sortedBy)
        {
            var count = source.Count();
            var items = await source.Skip((pageNumber - 1) * pageSize)
                .Take(pageSize).ToListAsync();

            return new PagedAndSortedList<T>(items, count, pageNumber, pageSize, search, sorted, sortedBy);
        }
    }
}