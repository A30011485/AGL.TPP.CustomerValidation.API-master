namespace AGL.TPP.CustomerValidation.API.Models
{
    using Destructurama.Attributed;

    /// <summary>
    /// Concession card class
    /// </summary>
    public class ConcessionCard
    {
        /// <summary>
        /// Concession Card number
        /// </summary>
        [LogMasked]
        public string CardNumber { get; set; }

        /// <summary>
        /// Concession card type
        /// </summary>
        public string CardType { get; set; }

        /// <summary>
        /// Concession card expiry date
        /// </summary>
        [LogMasked]
        public string DateOfExpiry { get; set; }
    }
}
