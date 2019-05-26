namespace EmailFunction.Core.Entities
{
    using EmailFunction.Core.ValueObjects;
    using MediatR;
    using System;

    public class EmailRequest : IRequest<string>
    {
        public EmailRequest(string fromAddress, string fromName, string toAddress)
        {
            FromAddress = new Email(fromAddress ?? throw new ArgumentNullException(nameof(fromAddress)));
            ToAddress = new Email(toAddress ?? throw new ArgumentNullException(nameof(toAddress)));
            FromName = fromName ?? throw new ArgumentNullException(nameof(fromName));
        }

        public Email ToAddress { get; private set; }

        public string Subject;

        public string PlainTextContent;

        public string HtmlContent;

        public string FromName { get; private set; }

        public Email FromAddress { get; private set; }
    }
}
