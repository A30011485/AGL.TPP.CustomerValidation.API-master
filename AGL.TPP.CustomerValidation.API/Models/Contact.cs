namespace AGL.TPP.CustomerValidation.API.Models
{
    using Destructurama.Attributed;

    /// <summary>
    /// Contact class
    /// </summary>
    public class Contact
    {
        /// <summary>
        /// Email address
        /// </summary>
        [LogMasked]
        public string EmailAddress { get; set; }

        /// <summary>
        /// Mobile phone
        /// </summary>
        [LogMasked]
        public string MobilePhone { get; set; }

        /// <summary>
        /// Home phone
        /// </summary>
        [LogMasked]
        public string HomePhone { get; set; }

        /// <summary>
        /// Work phone
        /// </summary>
        [LogMasked]
        public string WorkPhone { get; set; }
    }
}
