using System.Net.Http;
using System.Threading.Tasks;
using AGL.TPP.CustomerValidation.API.Providers.SapClient.Models;
using Serilog;

namespace AGL.TPP.CustomerValidation.API.Providers.SapClient
{
    public class SapPiClientMultipleMessage : SapPiClient, ISapPiClientMultipleMessage
    {
        public SapPiClientMultipleMessage(IHttpClient httpClient, ILogger logger) : base(httpClient, logger)
        {
        }

        public Task<SapPiMultipleMessageResponse<TResponseBody>> GetAsync<TResponseBody>(SapPiRequestHeadersContainer sapPiRequestHeadersContainer, string relativeUri)
        {
            return SendAsync<SapPiMultipleMessageResponse<TResponseBody>, object>(sapPiRequestHeadersContainer, relativeUri, HttpMethod.Get, null);
        }

        public Task<SapPiMultipleMessageResponse<TResponseBody>> PostAsync<TRequestBody, TResponseBody>(SapPiRequestHeadersContainer sapPiRequestHeadersContainer, string relativeUri, TRequestBody body)
        {
            return SendAsync<SapPiMultipleMessageResponse<TResponseBody>, TRequestBody>(sapPiRequestHeadersContainer, relativeUri, HttpMethod.Post, body);
        }

        public Task<SapPiMultipleMessageResponse<TResponseBody>> PostAsync<TResponseBody>(SapPiRequestHeadersContainer sapPiRequestHeadersContainer, string relativeUri)
        {
            return SendAsync<SapPiMultipleMessageResponse<TResponseBody>, object>(sapPiRequestHeadersContainer, relativeUri, HttpMethod.Post, null);
        }

        public Task<SapPiMultipleMessageResponse<TResponseBody>> DeleteAsync<TResponseBody>(SapPiRequestHeadersContainer sapPiRequestHeadersContainer, string relativeUri)
        {
            return SendAsync<SapPiMultipleMessageResponse<TResponseBody>, object>(sapPiRequestHeadersContainer, relativeUri, HttpMethod.Delete, null);
        }

        public Task<SapPiMultipleMessageResponse<TResponseBody>> PutAsync<TRequestBody, TResponseBody>(SapPiRequestHeadersContainer sapPiRequestHeadersContainer, string relativeUri, TRequestBody body)
        {
            return SendAsync<SapPiMultipleMessageResponse<TResponseBody>, TRequestBody>(sapPiRequestHeadersContainer, relativeUri, HttpMethod.Put, body);
        }

        public Task<SapPiMultipleMessageResponse<TResponseBody>> PutAsync<TResponseBody>(SapPiRequestHeadersContainer sapPiRequestHeadersContainer, string relativeUri)
        {
            return SendAsync<SapPiMultipleMessageResponse<TResponseBody>, object>(sapPiRequestHeadersContainer, relativeUri, HttpMethod.Put, null);
        }

        public Task<SapPiMultipleMessageResponse<TResponseBody>> PatchAsync<TRequestBody, TResponseBody>(SapPiRequestHeadersContainer sapPiRequestHeadersContainer, string relativeUri, TRequestBody body)
        {
            return SendAsync<SapPiMultipleMessageResponse<TResponseBody>, TRequestBody>(sapPiRequestHeadersContainer, relativeUri, new HttpMethod("PATCH"), body);
        }
    }
}
