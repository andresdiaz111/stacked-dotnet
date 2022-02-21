using System;
using StackedWebAPI.Models;

namespace StackedWebAPI.Services.Models
{

	/// <summary>
    /// Serializes Data for all service responses
    /// that use non-paginated
    /// </summary>
    /// <typeparam name="T"></typeparam>
	public class ServiceResult<T>
	{
		public bool IsSuccess { get; set; }
		public T Data { get; set; }
		public ServiceError Error { get; set; }
	}

	/// <summary>
    /// Serializes Data for all service responses
    /// that use paginated
    /// </summary>
    /// <typeparam name="T"></typeparam>
	public class PagedServiceResult<T>
    {
		public bool IsSuccess { get; set; }
		public PaginationResult<T> Data { get; set; }
		public ServiceError Error { get; set; }
	}

	/// <summary>
    /// Standard error model for the service layer
    /// </summary>
	public class ServiceError
	{
		public string Message { get; set; }
		public string Stracktrace { get; set; }
	}
}

