namespace AGL.TPP.CustomerValidation.API.Models
{
    using System.Runtime.Serialization;

    [DataContract]
    public partial class MoveDetail
    {
        [DataMember(Name = "moveIn")]
        public MoveDetailMoveIn MoveIn { get; set; }

        [DataMember(Name = "moveOut")]
        public MoveDetailMoveOut MoveOut { get; set; }
    }
}
