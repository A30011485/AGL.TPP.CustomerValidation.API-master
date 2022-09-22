using System;
using System.Linq;
using AGL.TPP.CustomerValidation.API.Helpers;

namespace AGL.TPP.CustomerValidation.API.Models
{
    /// <summary>
    /// Cancellation details class
    /// </summary>
    public class CancellationDetail
    {
        /// <summary>
        /// Date of cancellation
        /// </summary>
        public string DateOfCancellation { get; set; }

        /// <summary>
        /// Cancellation types
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Cancellation reason
        /// </summary>
        public string Reason { get; set; }

        /// <summary>
        /// Cancellation other reason. This is mandatory if reason is selected as other
        /// </summary>
        public string ReasonOther { get; set; }
    }
}
