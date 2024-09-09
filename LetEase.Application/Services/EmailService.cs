using System.Threading.Tasks;
using LetEase.Application.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Net.Mail;
using System.Net;

namespace LetEase.Application.Services
{
	public class EmailService : IEmailService
	{
		private readonly IConfiguration _configuration;

		public EmailService(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		public async Task SendEmailAsync(string to, string subject, string body)
		{
			var smtpServer = _configuration["EmailSettings:SmtpServer"];
			var smtpPort = int.Parse(_configuration["EmailSettings:SmtpPort"]);
			var smtpUsername = _configuration["EmailSettings:SmtpUsername"];
			var smtpPassword = _configuration["EmailSettings:SmtpPassword"];

			using var client = new SmtpClient(smtpServer, smtpPort)
			{
				Credentials = new NetworkCredential(smtpUsername, smtpPassword),
				EnableSsl = true
			};

			var mailMessage = new MailMessage
			{
				From = new MailAddress(smtpUsername),
				Subject = subject,
				Body = body,
				IsBodyHtml = true,
			};
			mailMessage.To.Add(to);

			await client.SendMailAsync(mailMessage);
		}
	}
}

