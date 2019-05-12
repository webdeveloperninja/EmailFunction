namespace EmailFunction.Commands
{
    using MediatR;
    using SendGrid;
    using SendGrid.Helpers.Mail;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    public class SendEmailRequest : IRequest<string>
    {
        public string To { get; set; }
        public string Body { get; set; }
    }

    public class SendEmailHandler : IRequestHandler<SendEmailRequest, string>
    {
        public async Task<string> Handle(SendEmailRequest request, CancellationToken cancellationToken)
        {
            var apiKey = Environment.GetEnvironmentVariable("SendGrid_Key", EnvironmentVariableTarget.Process);
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("robert.smith.developer@gmail.com");
            var subject = "Sending with Twilio SendGrid is Fun";
            var to = new EmailAddress(request.To);
            var htmlContent = request.Body;
            var msg = MailHelper.CreateSingleEmail(from, to, subject, subject, htmlContent);
            var response = await client.SendEmailAsync(msg);

            return "Ok";
        }
    }
}
