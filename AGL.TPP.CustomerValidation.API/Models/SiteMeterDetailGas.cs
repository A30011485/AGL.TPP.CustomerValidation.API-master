namespace AGL.TPP.CustomerValidation.API.Models
{
    using System.Runtime.Serialization;

    [DataContract]
    public partial class SiteMeterDetailGas
    { 
        [DataMember(Name="mirn")]
        public string Mirn { get; set; }
    }
}
