namespace AGL.TPP.CustomerValidation.API.Models
{
    using System.Runtime.Serialization;

    [DataContract]
    public class MoveDetailMoveIn
    {
        [DataMember(Name = "electricity")]
        public MoveDetailMoveInElectricity Electricity { get; set; }

        [DataMember(Name = "gas")]
        public MoveDetailMoveInGas Gas { get; set; }
    }
}
