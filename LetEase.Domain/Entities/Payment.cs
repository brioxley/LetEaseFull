using System;

namespace LetEase.Domain.Entities
{
	public class Payment
	{
		public int Id { get; set; }
		public int ContractId { get; set; }
		public Contract Contract { get; set; }
		public decimal Amount { get; set; }
		public DateTime PaymentDate { get; set; }
		public PaymentStatus Status { get; set; }
		public decimal CommissionAmount { get; set; }
		public decimal CompanyAmount { get; set; }
		public DateTime DueDate { get; set; }
		public DateTime? PaidDate { get; set; }
	}

	public enum PaymentStatus
	{
		Pending,
		Processed,
		Failed
	}
}

