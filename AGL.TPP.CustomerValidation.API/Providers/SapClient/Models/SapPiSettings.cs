namespace AGL.TPP.CustomerValidation.API.Providers.SapClient.Models
{
    public class SapPiSettings
    {
        public string Host { get; set; }
        public string Port { get; set; }
        public string CustomerValidationEndpoint { get; set; }
        public string UserId { get; set; }
        public string Password { get; set; }
        public string Timeout { get; set; }
    }
}
