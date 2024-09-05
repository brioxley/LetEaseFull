using System.Collections.Generic;
using System.Net;

namespace LetEase.Domain.Entities
{
	public class Property
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public Address Address { get; set; }
		public int CompanyId { get; set; }
		public Company Company { get; set; }
		public List<Room> Rooms { get; set; }
		public List<Amenity> Amenities { get; set; }
		public List<UserProperty> Managers { get; set; }
		public List<Images> images { get; set; }
	}
}
