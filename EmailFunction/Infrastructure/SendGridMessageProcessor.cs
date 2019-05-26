namespace EmailFunction.Infrastructure
{
    using global::EmailFunction.Core.Entities;
    using global::EmailFunction.Core.Interfaces;
    using SendGrid;
    using SendGrid.Helpers.Mail;
    using System;
    using System.Threading.Tasks;

    internal class SendGridMessageProcessor : IMessageProcessor
    {
        public async Task<Response> Send(EmailRequest request)
        {
            var apiKey = Environment.GetEnvironmentVariable("SENDGRID_KEY", EnvironmentVariableTarget.Process);

            var client = new SendGridClient(apiKey);
            var fromAddress = new EmailAddress(request.FromAddress.Address, request.FromName);
            var toAddress = new EmailAddress(request.ToAddress.Address);

            var message = MailHelper.CreateSingleEmail(fromAddress, toAddress, request.Subject, plainTextContent: request.PlainTextContent, htmlContent: request.HtmlContent);

            return await client.SendEmailAsync(message);
        }
    }
}
