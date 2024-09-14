using LetEase.Domain.Entities;
using LetEase.Domain.Converters;
using System.Text.Json.Serialization;

namespace LetEase.Application.DTOs
{
	public class RegisterUserDto
	{
		public string Username { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		[JsonConverter(typeof(StringEnumConverter<UserType>))]
		public UserType Type { get; set; }
		[JsonConverter(typeof(StringEnumConverter<UserRole>))]
		public UserRole? Role { get; set; }
		public int? CompanyId { get; set; }
	}
}
