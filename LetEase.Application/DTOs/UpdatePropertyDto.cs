namespace LetEase.Application.DTOs
{
	public class UpdatePropertyDto
	{
		public int Id { get; set; }
		public string Name { get; set; }
		// Add other properties that can be updated for a Property
		// For example:
		// public string Address { get; set; }
		// public int CompanyId { get; set; }
		// public List<int> AmenityIds { get; set; }
	}
}
