using SendGrid;
using System.Threading.Tasks;

namespace EmailFunction
{
    public class MessageRequest
    {
        public string To { get; set; }
        public string From { get; set; }
        public string Subject { get; set; }
        public string PlainTextContent { get; set; }
        public string HtmlContent { get; set; }
    }

    public interface IMessageProcessor
    {
        Task<Response> Send(MessageRequest request);
    }
}
