namespace EmailFunction.Core.Commands
{
    using EmailFunction.Core.Entities;
    using EmailFunction.Core.Interfaces;
    using MediatR;
    using System.Threading;
    using System.Threading.Tasks;

    public class SendEmailHandler : IRequestHandler<EmailRequest, string>
    {
        private IMessageProcessor _messageProcessor { get; set; }

        public SendEmailHandler(IMessageProcessor processor)
        {
            _messageProcessor = processor;
        }

        public async Task<string> Handle(EmailRequest request, CancellationToken cancellationToken)
        {
            var response = await _messageProcessor.Send(request);

            return $"Successfully sent email to {request.ToAddress.Address}";
        }
    }
}
