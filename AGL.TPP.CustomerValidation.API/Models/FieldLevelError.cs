namespace AGL.TPP.CustomerValidation.API.Models
{
    /// <summary>
    /// Field level errors
    /// </summary>
    public class FieldLevelError
    {
        /// <summary>
        /// Field name
        /// </summary>
        public string Field { get; set; }

        /// <summary>
        /// Error code
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Error message
        /// </summary>
        public string Message { get; set; }
    }
}
