using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Serilog;

namespace AGL.TPP.CustomerValidation.API.Security
{
    public class CertificateValidator
    {
        private readonly string[] _expectedIssuer;
        private readonly string[] _expectedSubjects;
        private readonly string[] _expectedPublicKeys;
        private readonly ILogger _logger;

        public CertificateValidator(ILogger logger, CertificateValidationOptions options)
        {
            _logger = logger;
            _expectedIssuer = options.Certificates.Select(c => c.Issuer)
                    .Select(Standardize)
                    .Where(NotEmpty)
                    .ToArray();

            // Filter with only non-empty values.
            _expectedSubjects = options.Certificates.SelectMany(c => c.Subject)
                .Select(Standardize)
                .Where(NotEmpty)
                .ToArray();

            // Filter with only non-empty values.
            _expectedPublicKeys = options.Certificates.Select(c => c.PublicKey)
                .Where(NotEmpty)
                .ToArray();
        }

        public bool CheckValid(X509Certificate2 certificate)
        {
            if (certificate == null)
            {
                return false;
            }

            if (!CheckValidDates(certificate))
            {
                _logger.Debug($"Certificate [{certificate.Thumbprint}] is not within a valid time window, possible certificate expiry.");
                return false;
            }

            if (!CheckValidSubject(certificate))
            {
                _logger.Debug($"Certificate [{certificate.Thumbprint}] does not have a matching Subject, possible malicious attack.");
                return false;
            }

            if (!CheckValidIssuer(certificate))
            {
                _logger.Debug($"Certificate [{certificate.Thumbprint}] does not have a matching Issuer, possible malicious attack.");
                return false;
            }

            if (!CheckValidPublicKey(certificate))
            {
                _logger.Debug($"Certificate [{certificate.Thumbprint}] does not have a matching PublicKey, possible malicious attack.");
                return false;
            }

            return true;
        }

        private bool CheckValidDates(X509Certificate2 certificate)
        {
            // Not before AND not after the certificate date.
            return !(DateTime.Compare(DateTime.UtcNow, certificate.NotBefore) < 0 || DateTime.Compare(DateTime.UtcNow, certificate.NotAfter) > 0);
        }

        private bool CheckValidPublicKey(X509Certificate2 certificate)
        {
            if (!_expectedPublicKeys.Any())
            {
                return true;
            }

            var certPublicKey = Convert.ToBase64String(certificate?.GetPublicKey());
            if (string.IsNullOrWhiteSpace(certPublicKey))
            {
                return false;
            }

            return _expectedPublicKeys.Contains(certPublicKey);
        }

        private bool CheckValidIssuer(X509Certificate2 certificate)
        {
            if (!_expectedIssuer.Any())
            {
                return true;
            }

            var certIssuerData = certificate.Issuer.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(Standardize);

            return certIssuerData
                .Any(issuer => _expectedIssuer.Contains(issuer));
        }

        private bool CheckValidSubject(X509Certificate2 certificate)
        {
            if (!_expectedSubjects.Any())
            {
                return true;
            }

            var certSubjectData = certificate.Subject.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(Standardize);

            return certSubjectData
                .Any(subject => _expectedSubjects.Contains(subject));
        }

        private static string Standardize(string value)
        {
            return (value ?? string.Empty).Trim().ToUpperInvariant();
        }

        private bool NotEmpty(string value)
        {
            return !string.IsNullOrWhiteSpace(value);
        }
    }
}
