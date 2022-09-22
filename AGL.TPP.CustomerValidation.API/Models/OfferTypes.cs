namespace AGL.TPP.CustomerValidation.API.Models
{
    using Newtonsoft.Json;
    using System.ComponentModel;

    /// <summary>
    /// Connection type enumeration
    /// </summary>
    [JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
    public enum OfferTypes
    {
        Unknown,

        [Description("ADF")]
        AcquisitionDualFuel,

        [Description("AEO")]
        AcquisitionElectricity,

        [Description("AGO")]
        AcquisitionGas,

        [Description("CSE")]
        CrossSellElectricity,

        [Description("CSG")]
        CrossSellGas,

        [Description("RDF")]
        RetentionDualFuel,

        [Description("REO")]
        RetentionElectricity,

        [Description("RGO")]
        RetentionGas
    }
}
