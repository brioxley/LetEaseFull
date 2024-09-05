using System.Collections.Generic;

namespace LetEase.Domain.Entities
{
	public class Company
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string ContactEmail { get; set; }
		public string PhoneNumber { get; set; }
		public List<User> Users { get; set; }
		public List<Property> Properties { get; set; }
		public CompanyLevel Level { get; set; }
		public decimal CommissionRate { get; set; }
	}


	public enum CompanyLevel
	{
		Owner,
		Standard
	}
}

