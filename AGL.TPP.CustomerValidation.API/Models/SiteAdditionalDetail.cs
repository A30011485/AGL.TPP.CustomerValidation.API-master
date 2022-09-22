namespace AGL.TPP.CustomerValidation.API.Models
{
    using System.Runtime.Serialization;

    [DataContract]
    public class SiteAdditionalDetail
    {
        [DataMember(Name = "addressPropertyUse")]
        public string AddressPropertyUse { get; set; }
    }
}
