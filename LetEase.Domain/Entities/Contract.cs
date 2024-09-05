using LetEase.Domain.Entities;

public class Contract
{
	public int Id { get; set; }
	public int ClientId { get; set; }
	public Client Client { get; set; }
	public int RoomId { get; set; }
	public Room Room { get; set; }
	public DateTime StartDate { get; set; }
	public DateTime EndDate { get; set; }
	public decimal TotalAmount { get; set; }
	public ContractStatus Status { get; set; }
	public int PropertyId { get; set; }
	public Property Property { get; set; }
	public decimal DepositAmount { get; set; }
	public bool IsSigned { get; set; }
	public int companyID { get; set; }
	public Company Company { get; set; }
}

public enum ContractStatus
	{
		Pending,
		Active,
		Completed,
		Cancelled
	}

