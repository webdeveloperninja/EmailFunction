namespace EmailFunction.Core.Interfaces
{
    using Microsoft.AspNetCore.Http;
    using System.Threading.Tasks;

    public interface IRequestDeserializer
    {
        Task<T> Deserialize<T>(HttpRequest request);
    }
}
