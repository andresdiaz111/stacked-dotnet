namespace StackedWebAPI.Models
{
	/// <summary>
    /// Data transfer object for a Blog User
    /// </summary>
	public class UserDto : BlogDtoModel
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string UserName { get; set; }
		public string Email { get; set; }
		public string PictureUrl { get; set; }
	}
}

