namespace AGL.TPP.CustomerValidation.API.Models
{
    using Newtonsoft.Json;
    using System.ComponentModel;

    /// <summary>
    /// Connection type enumeration
    /// </summary>
    [JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
    public enum CustomerTypes
    {
        [Description("RES")]
        RES,

        [Description("BUS")]
        BUS,
    }
}
