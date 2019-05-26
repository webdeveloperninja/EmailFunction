namespace EmailFunction.Core.Interfaces
{
    using EmailFunction.Core.Entities;
    using EmailFunction.Core.ValueObjects;
    using SendGrid;
    using System.Threading.Tasks;

    public interface IMessageProcessor
    {
        Task<Response> Send(EmailRequest request);
    }
}
