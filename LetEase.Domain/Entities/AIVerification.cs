using System;
using System.Collections.Generic;

namespace LetEase.Domain.Entities
{
	public class AIVerification
	{
		public int Id { get; set; }
		public int ClientId { get; set; }
		public Client Client { get; set; }
		public DateTime VerificationDate { get; set; }
		public string VerificationResult { get; set; }
		public double ConfidenceScore { get; set; }
		public List<string> Flags { get; set; }
	}
}
