using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Threading.Tasks;

namespace EmailFunction.Infrastructure
{
    class SendGrid : IMessageProcessor
    {
        public async Task<Response> Send(MessageRequest request)
        {
            var apiKey = Environment.GetEnvironmentVariable("SENDGRID_KEY", EnvironmentVariableTarget.Process);
            var fromAddress = Environment.GetEnvironmentVariable("FROM_ADDRESS", EnvironmentVariableTarget.Process);
            var fromName = Environment.GetEnvironmentVariable("FROM_NAME", EnvironmentVariableTarget.Process);

            var client = new SendGridClient(apiKey);
            var from = new EmailAddress(fromAddress, fromName);
            var to = new EmailAddress(request.To);
            var message = MailHelper.CreateSingleEmail(from, to, request.Subject, plainTextContent: request.Content, htmlContent: null);

            return await client.SendEmailAsync(message);
        }
    }
}
