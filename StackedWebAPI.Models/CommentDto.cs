namespace StackedWebAPI.Models
{
	/// <summary>
	/// Data transfer object for a Blog Comment
	/// </summary>
	public class CommentDto : BlogDtoModel
	{
		public string CommenterName { get; set; }
		public string Message { get; set; }
		public Guid ArticleId { get; set; }
		public ArticleDto Article { get; set; }
	}
}

