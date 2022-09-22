namespace AGL.TPP.CustomerValidation.API.Models
{
    using Destructurama.Attributed;

    /// <summary>
    /// Medicare details
    /// </summary>
    public class IdentificationDocumentMedicare
    {
        /// <summary>
        /// Medicare number
        /// </summary>
        [LogMasked]
        public string MedicareNumber { get; set; }

        /// <summary>
        /// Individual Reference Number
        /// </summary>
        [LogMasked]
        public string IndividualReferenceNumber { get; set; }
    }

    /// <summary>
    /// Medicare extensions
    /// </summary>
    public static class MedicareExtensions
    {
        /// <summary>
        /// Combines Medicare Number with Individual Reference Number
        /// </summary>
        /// <param name="medicare">Medicare object</param>
        /// <returns>Medicare number with Individual Reference Number</returns>
        public static string ToMedicareNumber(this IdentificationDocumentMedicare medicare)
        {
            return medicare?.MedicareNumber + medicare?.IndividualReferenceNumber;
        }
    }
}