namespace AGL.TPP.CustomerValidation.API.Models
{
    public class EventHubHeader
    {
        /// <summary>
        /// Vendor business partner number
        /// </summary>
        public string VendorBusinessPartnerNumber { get; set; }

        /// <summary>
        /// Vendor name
        /// </summary>
        public string VendorName { get; set; }

        /// <summary>
        /// Vendor lead id
        /// </summary>
        public string VendorLeadId { get; set; }

        /// <summary>
        /// Correlation Id
        /// </summary>
        public string CorrelationId { get; set; }

        /// <summary>
        /// Type of transaction - Sale, SDFI, Cancel, Moveout and Change
        /// </summary>
        public string TransactionType { get; set; }

        /// <summary>
        /// Event Hub Header Message type
        /// </summary>
        public string MessageType { get; set; }
    }
}
