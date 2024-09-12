using LetEase.Domain.Entities;

using LetEase.Domain.Entities;

namespace LetEase.Application.DTOs
{
	public class UserDto
	{
		public string Id { get; set; }
		public string UserName { get; set; }
		public string Email { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public UserType Type { get; set; }
		public UserRole? Role { get; set; }
		public int? CompanyId { get; set; }
	}
}
