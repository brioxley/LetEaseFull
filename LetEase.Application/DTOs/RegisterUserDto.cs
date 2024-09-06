using LetEase.Domain.Entities;

namespace LetEase.Application.DTOs
{
	public class RegisterUserDto
	{
		public string Username { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public UserType Type { get; set; }
		public UserRole? Role { get; set; }
		public int? CompanyId { get; set; }
	}
}
