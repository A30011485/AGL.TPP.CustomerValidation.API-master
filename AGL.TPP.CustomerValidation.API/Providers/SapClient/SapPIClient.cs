using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AGL.TPP.CustomerValidation.API.Providers.SapClient.Models;
using Microsoft.AspNetCore.ResponseCaching.Internal;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Serilog;

namespace AGL.TPP.CustomerValidation.API.Providers.SapClient
{
    public abstract class SapPiClient
    {
        private readonly IHttpClient _httpClient;
        private readonly ILogger _logger;

        private static readonly JsonSerializerSettings JsonSerializerSettings = new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            Converters = new List<JsonConverter>
            {
                new StringEnumConverter()
            }
        };

        protected SapPiClient(IHttpClient httpClient, ILogger logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        protected async Task<TEnvelope> SendAsync<TEnvelope, TRequestBody>(SapPiRequestHeadersContainer sapPiRequestHeadersContainer, string relativeUri, HttpMethod method, TRequestBody body)
        {
            var watch = Stopwatch.StartNew();
            _logger.Information("Sending {SapPiHttpMethod} to SAP PO {SapPiRelativeUri}", method, relativeUri);
            _logger.Debug("Sending {SapPiHttpMethod} to SAP PO {SapPiRelativeUri} with body {@SapPiRequestBody}", method, relativeUri, body);

            var path = new Uri(relativeUri, UriKind.Relative);

            var request = new HttpRequestMessage(method, path);

            AddRequestHeaders(request, sapPiRequestHeadersContainer);
            AddBody(body, request);

            string responseBody;

            HttpResponseMessage response = null;
            try
            {
                response = await _httpClient.SendAsync(request);
                responseBody = await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                _logger.Fatal(ex, "Error accessing SAP PO.");
                responseBody = "SAP - Internal server error";
            }

            watch.Stop();
            var elapsed = watch.Elapsed;

            if (response?.IsSuccessStatusCode == true)
            {
                _logger.Information("SAP PO request completed successfully with status code {SapPoStatusCode} in {SapPoElapsed}", (int)response.StatusCode, elapsed);
                _logger.Debug("SAP PO request completed successfully in {SapPoElapsed} with body {@SapPoResponseBody}", elapsed, responseBody);
            }
            else
            {
                _logger.Warning("SAP PO request failed with status code {SapPoStatusCode} took {SapPoElapsed} with body {@SapPIResponseBody}", response?.StatusCode, elapsed, responseBody);
                var errorJson = new { Return = new object[] { new { description = "SAP - Internal server error", Id = "SAP_EXCEPTION", Number = 5000, Type = "X" } } };
                responseBody = JsonConvert.SerializeObject(errorJson);
            }

            return JsonConvert.DeserializeObject<TEnvelope>(responseBody);
        }

        private static void AddRequestHeaders(HttpRequestMessage request, SapPiRequestHeadersContainer sapPiRequestHeadersContainer)
        {
            request.Headers.Add("Correlation-Id", sapPiRequestHeadersContainer.CorrelationId);

            foreach (var optionalHeader in sapPiRequestHeadersContainer.OptionalHeaders)
            {
                request.Headers.Add(optionalHeader.Key, optionalHeader.Value);
            }
        }

        private static void AddBody<T>(T body, HttpRequestMessage request)
        {
            if (body != null)
            {
                request.Content = new StringContent(JsonConvert.SerializeObject(body, JsonSerializerSettings), Encoding.UTF8, "application/json");
            }
        }
    }
}
