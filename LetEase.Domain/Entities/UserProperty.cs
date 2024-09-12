namespace LetEase.Domain.Entities
{
	public class UserProperty
	{
		public string UserId { get; set; }
		public User User { get; set; }
		public int PropertyId { get; set; }
		public Property Property { get; set; }
	}
}
