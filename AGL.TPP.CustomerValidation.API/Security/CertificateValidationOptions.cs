namespace AGL.TPP.CustomerValidation.API.Security
{
    public class CertificateValidationOptions
    {
        public bool Enabled { get; set; } = false;

        public CertificateDetail[] Certificates { get; set; } = new CertificateDetail[0];
    }
}