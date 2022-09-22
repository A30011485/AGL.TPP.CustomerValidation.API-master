using System.ComponentModel.DataAnnotations;

namespace AGL.TPP.CustomerValidation.API.Models
{
    using Newtonsoft.Json;
    using System.ComponentModel;

    /// <summary>
    /// Connection type enumeration
    /// </summary>
    [JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
    public enum CancellationReasons
    {
        Unknown,

        [Description("OutsideCoolOffPeriod")]
        [Display(Name = "CUSTOMER CANCELLATION - OUTSIDE COOL OFF PERIOD")]
        OutsideCoolOffPeriod,

        [Description("WithinCoolOffPeriod")]
        [Display(Name = "CUSTOMER CANCELLATION - WITHIN COOL OFF PERIOD")]
        WithinCoolOffPeriod,

        [Description("IncorrectPremise")]
        [Display(Name = "INCORRECT PERMISE")]
        IncorrectPremise,

        [Description("SiteWonInError")]
        [Display(Name = "SITE WON IN ERROR")]
        SiteWonInError,

        [Description("MoveInCancellation")]
        [Display(Name = "MOVE IN CANCELLATION")]
        MoveInCancellation,

        [Description("Other")]
        [Display(Name = "OTHER")]
        Other
    }
}
