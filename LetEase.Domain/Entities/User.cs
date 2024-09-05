using System;
using System.Collections.Generic;

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
		public bool EmailConfirmed { get; set; }
		public UserRole Role { get; set; }
		public int? CompanyId { get; set; }  // Nullable, as not all users will be associated with a company
		public Company Company { get; set; }
		public List<UserProperty> ManagedProperties { get; set; }
		public UserType Type { get; set; }  // New property to distinguish between company users, admins, and clients
	}

	public enum UserRole
	{
		Admin,
		Manager,
		Staff,
		Client  // Added client role
	}

	public enum UserType
	{
		CompanyUser,
		Admin,
		Client
	}
}
