namespace StackedWebAPI.Web.Models
{
	public class ManyArticlesRequest
	{
		public int Page { get; set; }
		public int PerPage { get; set; }
		public List<string> Tags { get; set; }
	}
}

