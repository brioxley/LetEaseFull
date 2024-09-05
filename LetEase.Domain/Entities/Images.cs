

using System.ComponentModel.DataAnnotations;

namespace LetEase.Domain.Entities
{
	public class Images
	{
		[Key]
		public int ImageId { get; set; }
		public string Imagelocation { get; set; }
	}
}
