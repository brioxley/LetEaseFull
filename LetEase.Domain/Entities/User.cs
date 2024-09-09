namespace LetEase.Domain.Entities
{
	public class User
	{
		public int Id { get; set; }
		public string Username { get; set; }
		public string Email { get; set; }
		public string PasswordHash { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public DateTime DateRegistered { get; set; }

		public UserType Type { get; set; }  // Now handles all user distinctions
		public UserRole Role { get; set; }  // Role within the company (if applicable)

		// Only relevant for CompanyUser
		public int? CompanyId { get; set; }
		public Company Company { get; set; }

		// Only relevant for CompanyUser/Manager/Staff
		public List<UserProperty> ManagedProperties { get; set; }

		//this is used for verification
		public bool EmailConfirmed { get; set; }
		public string EmailConfirmationToken { get; set; }
		public DateTime? EmailConfirmationTokenExpires { get; set; }
	}

	public enum UserRole
	{
		Manager,
		Staff,
		Client  // Role within the company or as a client
	}

	public enum UserType
	{
		Admin,
		CompanyUser,
		Client
	}
}

