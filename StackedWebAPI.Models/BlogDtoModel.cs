namespace StackedWebAPI.Models
{
	public abstract class BlogDtoModel
	{
		public Guid Id { get; set; }
		public DateTime CreatedOn { get; set; }
		public DateTime UpdateOn { get; set; }
	}
}