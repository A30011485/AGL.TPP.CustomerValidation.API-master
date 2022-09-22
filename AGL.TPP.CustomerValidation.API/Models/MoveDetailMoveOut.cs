namespace AGL.TPP.CustomerValidation.API.Models
{
    using System.Runtime.Serialization;

    [DataContract]
    public class MoveDetailMoveOut
    {
        [DataMember(Name = "electricity")]
        public MoveDetailMoveOutElectricity Electricity { get; set; }

        [DataMember(Name = "gas")]
        public MoveDetailMoveOutGas Gas { get; set; }
    }
}
