namespace EmailFunction.Infrastructure
{
    using global::EmailFunction.Core.Exceptions;
    using global::EmailFunction.Core.Interfaces;
    using Microsoft.AspNetCore.Http;
    using Newtonsoft.Json;
    using System.IO;
    using System.Threading.Tasks;

    public class RequestDeserializer : IRequestDeserializer
    {
        public async Task<T> Deserialize<T>(HttpRequest request)
        {
            try
            {
                var requestBody = await new StreamReader(request.Body).ReadToEndAsync();
                return JsonConvert.DeserializeObject<T>(requestBody);
            }
            catch
            {
                throw new RequestDeserializerException(request);
            }
        }
    }
}
