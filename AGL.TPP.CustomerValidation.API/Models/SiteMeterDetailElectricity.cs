namespace AGL.TPP.CustomerValidation.API.Models
{
    using System.Runtime.Serialization;

    [DataContract]
    public class SiteMeterDetailElectricity
    { 
        [DataMember(Name="nmi")]
        public string Nmi { get; set; }
    }
}
