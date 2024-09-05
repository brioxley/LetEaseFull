using System;

namespace LetEase.Domain.Entities
{
	public class Booking
	{
		public int Id { get; set; }
		public int RoomId { get; set; }
		public Room Room { get; set; }
		public int ClientId { get; set; }
		public Client Client { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		public BookingStatus Status { get; set; }
	}

	public enum BookingStatus
	{
		Pending,
		Confirmed,
		Cancelled
	}
}
