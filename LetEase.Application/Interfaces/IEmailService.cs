using System.Threading.Tasks;

namespace LetEase.Application.Interfaces
{
	public interface IEmailService
	{
		Task SendEmailAsync(string to, string subject, string body);
	}
}
