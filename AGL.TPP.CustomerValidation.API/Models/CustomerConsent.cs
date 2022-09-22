namespace AGL.TPP.CustomerValidation.API.Models
{
    /// <summary>
    /// Customer consent details
    /// </summary>
    public class CustomerConsent
    {
        /// <summary>
        /// E Billing consent
        /// </summary>
        public string HasGivenEBillingConsent { get; set; }

        /// <summary>
        /// Credit check consent
        /// </summary>
        public string HasGivenCreditCheckConsent { get; set; }
    }
}
