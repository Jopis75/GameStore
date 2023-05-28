using Application.Interfaces.Infrastructure;
using Application.Models.Email;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Net;

namespace Infrastructure.Email
{
    public class EmailSender : IEmailSender
    {
        private readonly EmailSettings _emailSettings;

        public EmailSender(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }

        public async Task<bool> SendEmail(Application.Models.Email.Email email)
        {
            var sendGridClient = new SendGridClient(_emailSettings.ApiKey);
            var fromEmailAddress = new EmailAddress
            {
                Email = _emailSettings.FromAddress,
                Name = _emailSettings.FromName
            };
            var toEmailAddress = new EmailAddress(email.To);

            var sendGridMessage = MailHelper.CreateSingleEmail(fromEmailAddress, toEmailAddress, email.Subject, email.Body, email.Body);
            var response = await sendGridClient.SendEmailAsync(sendGridMessage);

            return response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.Accepted;
        }
    }
}
