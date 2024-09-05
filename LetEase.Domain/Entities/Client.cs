using System.Collections.Generic;

namespace LetEase.Domain.Entities
{
	public class Client
	{
		public int Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
		public string PhoneNumber { get; set; }
		public string PassportNumber { get; set; }
		public string DrivingLicenseNumber { get; set; }
		public EmergencyContact EmergencyContact { get; set; }
		public List<Reference> References { get; set; }
		public List<Contract> Contracts { get; set; }
		public VerificationStatus VerificationStatus { get; set; }
	}

	public enum VerificationStatus
	{
		Pending,
		Verified,
		Rejected
	}
}
