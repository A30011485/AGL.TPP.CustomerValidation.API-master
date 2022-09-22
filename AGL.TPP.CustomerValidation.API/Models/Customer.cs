namespace AGL.TPP.CustomerValidation.API.Models
{
    using Destructurama.Attributed;

    /// <summary>
    /// Customer class
    /// </summary>
    public class Customer
    {
        /// <summary>
        /// Title
        /// </summary>
        [LogMasked]
        public string Title { get; set; }

        /// <summary>
        /// First name
        /// </summary>
        [LogMasked]
        public string FirstName { get; set; }

        /// <summary>
        /// Middle name
        /// </summary>
        [LogMasked]
        public string MiddleName { get; set; }

        /// <summary>
        /// Last name
        /// </summary>
        [LogMasked]
        public string LastName { get; set; }

        /// <summary>
        /// Date of birth
        /// </summary>
        [LogMasked]
        public string DateOfBirth { get; set; }
    }
}
