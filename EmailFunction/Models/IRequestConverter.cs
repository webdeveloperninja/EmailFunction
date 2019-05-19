namespace EmailFunction.Models
{
    using Microsoft.AspNetCore.Http;
    using System.Threading.Tasks;

    public interface IRequestConverter
    {
        Task<EmailRequest> Convert(HttpRequest request);
    }
}
