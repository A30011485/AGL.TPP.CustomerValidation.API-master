using System.Text.RegularExpressions;

namespace AGL.TPP.CustomerValidation.API.Models
{
    using Destructurama.Attributed;

    /// <summary>
    /// Street address
    /// </summary>
    public class StreetAddress
    {
        private string _state;
        private string _streetName;
        private string _streetNumber;
        private string _suburb;

        /// <summary>
        /// Building name
        /// </summary>
        [LogMasked]
        public string BuildingName { get; set; }

        /// <summary>
        /// Floor number
        /// </summary>
        [LogMasked]
        public string FloorNumber { get; set; }

        /// <summary>
        /// This field to use for backend (SAP) to ignore the validation for MSAT verified addresses.
        /// When its true, it will be passed to backend (SAP), then ignores regional file check.
        /// </summary>
        public bool IgnoreAddressValidation { get; set; }

        /// <summary>
        /// Lot number
        /// </summary>
        [LogMasked]
        public string LotNumber { get; set; }

        /// <summary>
        /// Postcode
        /// </summary>
        public string Postcode { get; set; }

        /// <summary>
        /// Post office box number
        /// </summary>
        public string PostOfficeBoxNumber { get; set; }

        /// <summary>
        /// State
        /// </summary>
        [LogMasked]
        public string State
        {
            get => _state;
            set => _state = TrimExtraSpace(value);
        }

        /// <summary>
        /// Street name
        /// </summary>
        [LogMasked]
        public string StreetName
        {
            get => _streetName;
            set => _streetName = TrimExtraSpace(value);
        }

        /// <summary>
        /// Street number
        /// </summary>
        [LogMasked]
        public string StreetNumber
        {
            get => _streetNumber;
            set => _streetNumber = TrimExtraSpace(value);
        }

        /// <summary>
        /// Suburb
        /// </summary>
        [LogMasked]
        public string Suburb
        {
            get => _suburb;
            set => _suburb = TrimExtraSpace(value);
        }

        /// <summary>
        /// Unit number
        /// </summary>
        [LogMasked]
        public string UnitNumber { get; set; }

        private static string TrimExtraSpace(string value)
        {
            if (value == null) return null;
            return Regex.Replace(value, @"\s+", " ").Trim();
        }
    }
}
