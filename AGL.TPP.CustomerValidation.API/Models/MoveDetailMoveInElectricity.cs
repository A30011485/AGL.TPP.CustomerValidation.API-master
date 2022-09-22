namespace AGL.TPP.CustomerValidation.API.Models
{
    using System.Runtime.Serialization;

    [DataContract]
    public class MoveDetailMoveInElectricity
    {
        [DataMember(Name = "date")]
        public string Date { get; set; }
    }
}
