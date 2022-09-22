using System.Threading.Tasks;
using AGL.TPP.CustomerValidation.API.Providers.SapClient.Models;

namespace AGL.TPP.CustomerValidation.API.Providers.SapClient
{
    public interface ISapPiClientSingleMessage
    {
        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="TResponseBody"></typeparam>
        /// <param name="sapPiRequestHeadersContainer"></param>
        /// <param name="relativeUri">Given the URI is 'https://sap-pi.agl.com.au/v1/contract' you should
        /// provide 'v1/contract'.</param>
        /// <returns></returns>
        Task<SapPiSingleMessageResponse<TResponseBody>> GetAsync<TResponseBody>(SapPiRequestHeadersContainer sapPiRequestHeadersContainer, string relativeUri);

        /// <summary>
        /// Use this overload when you don't have a request body.
        /// </summary>
        /// <typeparam name="TResponseBody"></typeparam>
        /// <param name="sapPiRequestHeadersContainer"></param>
        /// <param name="relativeUri">Given the URI is 'https://sap-pi.agl.com.au/v1/contract' you should
        /// provide 'v1/contract'.</param>
        /// <returns></returns>
        Task<SapPiSingleMessageResponse<TResponseBody>> PostAsync<TResponseBody>(SapPiRequestHeadersContainer sapPiRequestHeadersContainer, string relativeUri);

        /// <summary>
        /// Use this overload when you don't expect a Response body.
        /// </summary>
        /// <typeparam name="TRequestBody"></typeparam>
        /// <param name="sapPiRequestHeadersContainer"></param>
        /// <param name="relativeUri">Given the URI is 'https://sap-pi.agl.com.au/v1/contract' you should
        /// provide 'v1/contract'.</param>
        /// <param name="requestBody"></param>
        /// <returns></returns>
        Task<SapPiSingleMessageResponse> PostAsync<TRequestBody>(SapPiRequestHeadersContainer sapPiRequestHeadersContainer, string relativeUri, TRequestBody requestBody);

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="TRequestBody"></typeparam>
        /// <typeparam name="TResponseBody"></typeparam>
        /// <param name="sapPiRequestHeadersContainer"></param>
        /// <param name="relativeUri">Given the URI is 'https://sap-pi.agl.com.au/v1/contract' you should
        /// provide 'v1/contract'.</param>
        /// <param name="body"></param>
        /// <returns></returns>
        Task<SapPiSingleMessageResponse<TResponseBody>> PostAsync<TRequestBody, TResponseBody>(SapPiRequestHeadersContainer sapPiRequestHeadersContainer,
            string relativeUri, TRequestBody body);

        /// <summary>
        /// Use this overload when you don't have a request body.
        /// </summary>
        /// <typeparam name="TResponseBody"></typeparam>
        /// <param name="sapPiRequestHeadersContainer"></param>
        /// <param name="relativeUri">Given the URI is 'https://sap-pi.agl.com.au/v1/contract' you should
        /// provide 'v1/contract'.</param>
        /// <returns></returns>
        Task<SapPiSingleMessageResponse<TResponseBody>> PutAsync<TResponseBody>(SapPiRequestHeadersContainer sapPiRequestHeadersContainer, string relativeUri);

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="TRequestBody"></typeparam>
        /// <typeparam name="TResponseBody"></typeparam>
        /// <param name="sapPiRequestHeadersContainer"></param>
        /// <param name="relativeUri">Given the URI is 'https://sap-pi.agl.com.au/v1/contract' you should
        /// provide 'v1/contract'.</param>
        /// <param name="body"></param>
        /// <returns></returns>
        Task<SapPiSingleMessageResponse<TResponseBody>> PutAsync<TRequestBody, TResponseBody>(SapPiRequestHeadersContainer sapPiRequestHeadersContainer,
            string relativeUri, TRequestBody body);

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="TResponseBody"></typeparam>
        /// <param name="sapPiRequestHeadersContainer"></param>
        /// <param name="relativeUri">Given the URI is 'https://sap-pi.agl.com.au/v1/contract' you should
        /// provide 'v1/contract'.</param>
        /// <returns></returns>
        Task<SapPiSingleMessageResponse<TResponseBody>> DeleteAsync<TResponseBody>(SapPiRequestHeadersContainer sapPiRequestHeadersContainer, string relativeUri);

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="TRequestBody"></typeparam>
        /// <typeparam name="TResponseBody"></typeparam>
        /// <param name="sapPiRequestHeadersContainer"></param>
        /// <param name="relativeUri">Given the URI is 'https://sap-pi.agl.com.au/v1/contract' you should
        /// provide 'v1/contract'.</param>
        /// <param name="body"></param>
        /// <returns></returns>
        Task<SapPiSingleMessageResponse<TResponseBody>> PatchAsync<TRequestBody, TResponseBody>(SapPiRequestHeadersContainer sapPiRequestHeadersContainer,
            string relativeUri, TRequestBody body);
    }
}
