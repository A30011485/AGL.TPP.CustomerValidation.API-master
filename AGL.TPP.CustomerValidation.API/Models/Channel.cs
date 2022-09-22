using System.ComponentModel;

namespace AGL.TPP.CustomerValidation.API.Models
{
    /// <summary>
    /// Business Channels
    /// </summary>
    public enum Channel
    {
        Unknown,
        [Description("TM")]
        TeleMarketer,
        [Description("FIELD")]
        OnField,
        [Description("Web")]
        WEB,
        [Description("Intervention")]
        INTERVENTION,
        [Description("Comparator")]
        COMPARATOR,
        [Description("Broker")]
        BROKER,
        [Description("Association")]
        ASSOCIATION,
        [Description("TPA")]
        ThirdPartyAggregator,
        [Description("Reactivation")]
        REACTIVATION,
        [Description("New Energy")]
        NEWENERGY
    }
}