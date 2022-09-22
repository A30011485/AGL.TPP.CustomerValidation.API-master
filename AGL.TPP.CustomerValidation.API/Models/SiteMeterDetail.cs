namespace AGL.TPP.CustomerValidation.API.Models
{
    using System.Runtime.Serialization;

    [DataContract]
    public class SiteMeterDetail
    { 
        [DataMember(Name="electricity")]
        public SiteMeterDetailElectricity Electricity { get; set; }

        [DataMember(Name="gas")]
        public SiteMeterDetailGas Gas { get; set; }
    }
}
