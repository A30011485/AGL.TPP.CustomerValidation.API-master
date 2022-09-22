using AGL.TPP.CustomerValidation.API.Providers.EmailDomainValidationClient.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using Stopwatch = System.Diagnostics.Stopwatch;


namespace AGL.TPP.CustomerValidation.API.Providers.EmailDomainValidationClient
{


    public class EmailDomainValidationClient: IEmailDomainValidationClient
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger _logger;
        private readonly string subscriptionKeyName = "Ocp-Apim-Subscription-Key";
        private readonly IOptions<EmailDomainValidationApiSettings> _domainValidationApiSettings;


        public EmailDomainValidationClient(
            HttpClient httpClient, 
            IOptions<EmailDomainValidationApiSettings> domainValidationApiSettings, 
            ILogger logger)
        {
            _httpClient = httpClient;
            _domainValidationApiSettings = domainValidationApiSettings;
            _logger = logger;
        }

        public async Task<EmailDomainValidationClientResponse> ValidateDomainAsync(string domainInput, string correlationId)
        {

            var queryString = HttpUtility.ParseQueryString(string.Empty);
            var apiSettings = _domainValidationApiSettings.Value;

            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            // Request headers
            _httpClient.DefaultRequestHeaders.Add("Correlation-Id", correlationId);
            _httpClient.DefaultRequestHeaders.Add(subscriptionKeyName, apiSettings.SubscriptionKey);
            _httpClient.DefaultRequestHeaders.Add("Kasada-Bypass-Secret-Key", apiSettings.KasadaBypassSecretKey);

            // Request parameters
            queryString["domainName"] = domainInput;
            var uri = apiSettings.Endpoint + "?" + queryString;

            var watch = Stopwatch.StartNew();
            _logger.Information("Sending {HttpMethod} to DomainValidationApi {RelativeUri}", HttpMethod.Get, uri);

            string responseBody;
            HttpResponseMessage response = null;

            try
            {
                response = await _httpClient.GetAsync(uri);
                responseBody = await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                _logger.Fatal(ex, "Error accessing Domain validation api.");
                responseBody = "Domain validation api - Internal server error";
            }

            watch.Stop();
            var elapsed = watch.Elapsed;

            if (response?.IsSuccessStatusCode == true)
            {
                _logger.Information("Domain validation api request completed successfully with status code {StatusCode} in {Elapsed}", (int)response.StatusCode, elapsed);
                _logger.Debug("Domain validation api request completed successfully in {Elapsed} with body {@ResponseBody}", elapsed, responseBody);
            }
            else
            {
                _logger.Warning("Domain validation api request failed with status code {StatusCode} took {Elapsed} with body {@ResponseBody}", response?.StatusCode, elapsed, responseBody);
                var errorJson = new { Return = new object[] { new { description = "Domain validation api - Internal server error", Id = "SAP_EXCEPTION", Number = 5000, Type = "X" } } };
                responseBody = JsonConvert.SerializeObject(errorJson);
            }

            return JsonConvert.DeserializeObject<EmailDomainValidationClientResponse>(responseBody);
        }
    }
}
