using System;

namespace LetEase.Domain.Entities
{
	public class ActivityLog
	{
		public int Id { get; set; }
		public int? UserId { get; set; }
		public User User { get; set; }
		public string EntityName { get; set; }
		public int EntityId { get; set; }
		public string Action { get; set; }
		public string Details { get; set; }
		public DateTime Timestamp { get; set; }
		public string IpAddress { get; set; }
	}
}
