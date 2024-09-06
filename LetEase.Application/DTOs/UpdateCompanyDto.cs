namespace LetEase.Application.DTOs
{
	public class UpdateCompanyDto
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string ContactEmail { get; set; }
		public string PhoneNumber { get; set; }
		// Add other properties that can be updated for a Company
		// For example:
		// public CompanyLevel Level { get; set; }
		// public decimal CommissionRate { get; set; }
	}
}
