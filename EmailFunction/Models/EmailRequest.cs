using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace EmailFunction.Models
{
    public class EmailRequest
    {
        public string To { get; set; }

        public string Subject { get; set; }

        public string PlainTextContent { get; set; }

        public async Task<EmailRequest> Create(HttpRequest req, IRequestConverter converter)
        {
            return await converter.Convert(req);
        }
    }
}
