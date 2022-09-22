namespace AGL.TPP.CustomerValidation.API.Security
{
    public class CertificateDetail
    {
        public string Issuer { get; set; }
        public string[] Subject { get; set; } = new string[0];
        public string PublicKey { get; set; }
    }
}