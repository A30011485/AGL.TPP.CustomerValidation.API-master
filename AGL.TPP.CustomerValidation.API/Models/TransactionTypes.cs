using System.ComponentModel;

namespace AGL.TPP.CustomerValidation.API.Models
{
    /// <summary>
    /// Connection type enumeration
    /// </summary>
    public enum TransactionTypes
    {
        Unknown,
        [Description("Sale")]
        Sale,
        [Description("SDFI")]
        SDFI,
        [Description("Cancel")]
        Cancel,
        [Description("Change")]
        Change,
        [Description("MoveOut")]
        MoveOut
    }
}
