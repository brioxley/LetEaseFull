using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using LetEase.Application.Interfaces;
using Microsoft.Extensions.Configuration;

namespace LetEase.Application.Services
{
	public class EmailService :	IEmailService
	{
		private readonly IConfiguration _configuration;

		public EmailService(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		public async Task SendEmailAsync(string to, string subject, string body)
		{
			var smtpServer = _configuration["EmailSettings:SmtpServer"];
			var port = int.Parse(_configuration["EmailSettings:SmtpPort"]);
			var username = _configuration["EmailSettings:SmtpUsername"];
			var password = _configuration["EmailSettings:SmtpPassword"];

			using var client = new SmtpClient(smtpServer, port)
			{
				Credentials = new NetworkCredential(username, password),
				EnableSsl = true
			};

			var mailMessage = new MailMessage
			{
				From = new MailAddress(username),
				Subject = subject,
				Body = body,
				IsBodyHtml = true
			};
			mailMessage.To.Add(to);

			await client.SendMailAsync(mailMessage);
		}
	}
}

