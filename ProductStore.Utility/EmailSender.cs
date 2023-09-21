using Microsoft.AspNetCore.Identity.UI.Services;

namespace ProductStore.Utility
{
	public class EmailSender : IEmailSender
	{
		public Task SendEmailAsync(string email, string subject, string htmlMessage)
		{
			//logic
			return Task.CompletedTask;
		}
	}
}
