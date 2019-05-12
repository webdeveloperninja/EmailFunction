namespace EmailFunction.Commands
{
    using MediatR;
    using System.Threading;
    using System.Threading.Tasks;

    public class SendEmailRequest : IRequest<string>
    {
        public string To { get; set; }
        public string Body { get; set; }
    }

    public class SendEmailHandler : IRequestHandler<SendEmailRequest, string>
    {
        public Task<string> Handle(SendEmailRequest request, CancellationToken cancellationToken)
        {
            return Task.FromResult("Pong");
        }
    }
}
