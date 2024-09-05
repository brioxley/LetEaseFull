using System;

namespace LetEase.Domain.Entities
{
	public class Document
	{
		public int Id { get; set; }
		public int ClientId { get; set; }
		public Client Client { get; set; }
		public string FileName { get; set; }
		public string FileType { get; set; }
		public string FileUrl { get; set; }
		public DocumentType Type { get; set; }
		public DateTime UploadDate { get; set; }
	}

	public enum DocumentType
	{
		Passport,
		DrivingLicense,
		Reference,
		Other
	}
}
