namespace AGL.TPP.CustomerValidation.API.Models
{
    using System.Runtime.Serialization;

    [DataContract]
    public class MoveDetailMoveOutGas
    {
        [DataMember(Name = "date")]
        public string Date { get; set; }
    }
}
