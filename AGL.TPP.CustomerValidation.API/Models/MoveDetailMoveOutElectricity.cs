namespace AGL.TPP.CustomerValidation.API.Models
{
    using System.Runtime.Serialization;

    [DataContract]
    public class MoveDetailMoveOutElectricity
    {
        [DataMember(Name = "date")]
        public string Date { get; set; }
    }
}
