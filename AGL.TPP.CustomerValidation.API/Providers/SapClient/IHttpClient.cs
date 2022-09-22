using System.Net.Http;
using System.Threading.Tasks;

namespace AGL.TPP.CustomerValidation.API.Providers.SapClient
{
    public interface IHttpClient
    {
        Task<HttpResponseMessage> SendAsync(HttpRequestMessage request);
    }
}
