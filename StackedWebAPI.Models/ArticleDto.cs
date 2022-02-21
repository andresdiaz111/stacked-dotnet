namespace StackedWebAPI.Models
{
	/// <summary>
	/// Data transfer object for a Blog Article
	/// </summary>
	public class ArticleDto : BlogDtoModel
	{
		public string Title { get; set; }
		public bool IsPublished { get; set; }
		public string Content { get; set; }
		public string AuthorId { get; set; }
		public string AuthorName { get; set; }
		public List<CommentDto> Comments { get; set; }
		public List<string> Tags { get; set; }
	}
}

