namespace EmailFunction.Models
{
    using Microsoft.AspNetCore.Http;
    using Newtonsoft.Json;
    using System.IO;
    using System.Threading.Tasks;

    public class EmailRequestConverter
    {
        public async Task<EmailRequest> Convert(HttpRequest request)
        {
            var requestBody = await new StreamReader(request.Body).ReadToEndAsync();
            var emailRequest = JsonConvert.DeserializeObject<EmailRequest>(requestBody);
            return emailRequest;
        }
    }
}
