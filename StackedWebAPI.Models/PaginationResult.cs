using System;
namespace StackedWebAPI.Models
{
    /// <summary>
    /// Data Model for paginated data
    /// </summary>
    /// <typeparam name="T"></typeparam>
	public class PaginationResult<T>
    {
		public long TotalCount { get; set; }
        public List<T> Results { get; set; }
        public int ResultPerPage { get; set; }
        public long PageNumber { get; set; }
    }
}