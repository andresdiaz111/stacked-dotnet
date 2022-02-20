using System;
namespace StackedWebAPI.Data.Models
{
	public abstract class EntityModel
	{
		public Guid Id { get; set; }
		public DateTime CreatedOn { get; set; }
		public DateTime UpdatedOn { get; set; }
	}
}

