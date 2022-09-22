namespace AGL.TPP.CustomerValidation.API.Models
{
    /// <summary>
    /// Contact class
    /// </summary>
    public class SapErrorMessage
    {
        /// <summary>
        /// Api error code
        /// </summary>
        public int ApiCode { get; set; }

        /// <summary>
        /// Sap error code
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// Sap error message class
        /// </summary>
        public string Class { get; set; }

        /// <summary>
        /// Sap error message
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Boolean that indicates whether a param replace is required
        /// </summary>
        public bool IsParamReplaceRequired { get; set; }
    }
}
