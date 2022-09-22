using System.Net.Http;
using System.Threading.Tasks;
using AGL.TPP.CustomerValidation.API.Providers.SapClient.Models;
using Serilog;

namespace AGL.TPP.CustomerValidation.API.Providers.SapClient
{
    public class SapPiClientSingleMessage : SapPiClient, ISapPiClientSingleMessage
    {
        public SapPiClientSingleMessage(IHttpClient httpClient, ILogger logger) : base(httpClient, logger)
        {
        }

        public Task<SapPiSingleMessageResponse<TResponseBody>> GetAsync<TResponseBody>(SapPiRequestHeadersContainer sapPiRequestHeadersContainer, string relativeUri)
        {
            return SendAsync<SapPiSingleMessageResponse<TResponseBody>, object>(sapPiRequestHeadersContainer, relativeUri, HttpMethod.Get, null);
        }

        public Task<SapPiSingleMessageResponse> PostAsync<TRequestBody>(SapPiRequestHeadersContainer sapPiRequestHeadersContainer, string relativeUri, TRequestBody requestBody)
        {
            return SendAsync<SapPiSingleMessageResponse, TRequestBody>(sapPiRequestHeadersContainer, relativeUri, HttpMethod.Post, requestBody);
        }

        public Task<SapPiSingleMessageResponse<TResponseBody>> PostAsync<TRequestBody, TResponseBody>(SapPiRequestHeadersContainer sapPiRequestHeadersContainer, string relativeUri, TRequestBody body)
        {
            return SendAsync<SapPiSingleMessageResponse<TResponseBody>, TRequestBody>(sapPiRequestHeadersContainer, relativeUri, HttpMethod.Post, body);
        }

        public Task<SapPiSingleMessageResponse<TResponseBody>> PostAsync<TResponseBody>(SapPiRequestHeadersContainer sapPiRequestHeadersContainer, string relativeUri)
        {
            return SendAsync<SapPiSingleMessageResponse<TResponseBody>, object>(sapPiRequestHeadersContainer, relativeUri, HttpMethod.Post, null);
        }

        public Task<SapPiSingleMessageResponse<TResponseBody>> DeleteAsync<TResponseBody>(SapPiRequestHeadersContainer sapPiRequestHeadersContainer, string relativeUri)
        {
            return SendAsync<SapPiSingleMessageResponse<TResponseBody>, object>(sapPiRequestHeadersContainer, relativeUri, HttpMethod.Delete, null);
        }

        public Task<SapPiSingleMessageResponse<TResponseBody>> PutAsync<TRequestBody, TResponseBody>(SapPiRequestHeadersContainer sapPiRequestHeadersContainer, string relativeUri, TRequestBody body)
        {
            return SendAsync<SapPiSingleMessageResponse<TResponseBody>, TRequestBody>(sapPiRequestHeadersContainer, relativeUri, HttpMethod.Put, body);
        }

        public Task<SapPiSingleMessageResponse<TResponseBody>> PutAsync<TResponseBody>(SapPiRequestHeadersContainer sapPiRequestHeadersContainer, string relativeUri)
        {
            return SendAsync<SapPiSingleMessageResponse<TResponseBody>, object>(sapPiRequestHeadersContainer, relativeUri, HttpMethod.Put, null);
        }

        public Task<SapPiSingleMessageResponse<TResponseBody>> PatchAsync<TRequestBody, TResponseBody>(SapPiRequestHeadersContainer sapPiRequestHeadersContainer, string relativeUri, TRequestBody body)
        {
            return SendAsync<SapPiSingleMessageResponse<TResponseBody>, TRequestBody>(sapPiRequestHeadersContainer, relativeUri, new HttpMethod("PATCH"), body);
        }
    }
}
