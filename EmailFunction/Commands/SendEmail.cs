namespace EmailFunction.Commands
{
    using MediatR;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    public class SendEmailRequest : IRequest<string>
    {
        public string To { get; set; }
        public string Subject { get; set; }
        public string PlainTextContent { get; set; }
    }

    public class SendEmailHandler : IRequestHandler<SendEmailRequest, string>
    {
        private IMessageProcessor _processor { get; set; }

        public SendEmailHandler(IMessageProcessor processor)
        {
            _processor = processor;
        }

        public async Task<string> Handle(SendEmailRequest request, CancellationToken cancellationToken)
        {
            var fromEmail = Environment.GetEnvironmentVariable("From_Email", EnvironmentVariableTarget.Process);

            var messageRequest = new MessageRequest
            {
                To = request.To,
                From = fromEmail,
                Subject = request.Subject,
                Content = request.PlainTextContent
            };

            var response = await _processor.Send(messageRequest);

            return $"Successfully sent email to {request.To}";
        }
    }
}
