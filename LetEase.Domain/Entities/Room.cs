using System.Collections.Generic;

namespace LetEase.Domain.Entities
{
	public class Room
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public decimal PricePerNight { get; set; }
		public int PropertyId { get; set; }
		public Property Property { get; set; }
		public List<Booking> Bookings { get; set; }
	}
}
