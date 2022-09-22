using System;
using AGL.TPP.CustomerValidation.API.Helpers;
using Destructurama.Attributed;

namespace AGL.TPP.CustomerValidation.API.Models
{
    /// <summary>
    /// Customer validation header model
    /// </summary>
    public class CustomerValidationHeaderModel
    {
        /// <summary>
        /// Originating sales vendor which is unique for each vendor business partner
        /// </summary>
        public string VendorBusinessPartnerNumber { get; set; }

        /// <summary>
        /// Originating sales vendor name
        /// </summary>
        public string VendorName { get; set; }

        private string _channel;

        /// <summary>
        /// Sale originating channel - Refer <cref name="Channel"></cref> for more details
        /// </summary>
        public string Channel
        {
            get
            {
                Enum.TryParse(_channel, true, out Channel c);
                return EnumHelper.GetDescription(c);
            }
            set
            {
                _channel = value;
            }
        }

        /// <summary>
        /// Retailer - AGL or PD
        /// </summary>
        public string Retailer { get; set; }

        /// <summary>
        /// Type of transaction - Refer <cref name="TransactionTypes"></cref> for more details
        /// </summary>
        public string TransactionType { get; set; }

        /// <summary>
        /// Unique value for each record and unique for each vendor
        /// </summary>
        public string VendorLeadId { get; set; }

        /// <summary>
        /// Resubmission count
        /// </summary>
        public string ResubmissionCount { get; set; }

        /// <summary>
        /// Resubmission comment
        /// </summary>
        [LogMasked]
        public string ResubmissionComments { get; set; }

        private string _offerType;

        /// <summary>
        /// Offer types - Refer <cref name="OfferTypes"></cref> for more details
        /// </summary>
        public string OfferType
        {
            get
            {
                Enum.TryParse(_offerType, true, out OfferTypes ot);
                return EnumHelper.GetDescription(ot);
            }
            set
            {
                _offerType = value;
            }
        }

        /// <summary>
        /// Date of sale
        /// </summary>
        public string DateOfSale { get; set; }
    }
}
