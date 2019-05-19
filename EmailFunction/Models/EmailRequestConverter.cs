namespace EmailFunction.Models
{
    using EmailFunction.Exceptions;
    using Microsoft.AspNetCore.Http;
    using Newtonsoft.Json;
    using System;
    using System.IO;
    using System.Threading.Tasks;

    public class EmailRequestConverter : IRequestConverter
    {
        public async Task<EmailRequest> Convert(HttpRequest request)
        {
            try
            {
                var requestBody = await new StreamReader(request.Body).ReadToEndAsync();
                var emailRequest = JsonConvert.DeserializeObject<EmailRequest>(requestBody);

                if (string.IsNullOrEmpty(emailRequest.Subject) || 
                    string.IsNullOrEmpty(emailRequest.To) || 
                    string.IsNullOrEmpty(emailRequest.PlainTextContent))
                {
                    throw new Exception();
                }

                return emailRequest;
            }
            catch
            {
                throw new RequestConverterException(request);
            }
        }
    }
}
