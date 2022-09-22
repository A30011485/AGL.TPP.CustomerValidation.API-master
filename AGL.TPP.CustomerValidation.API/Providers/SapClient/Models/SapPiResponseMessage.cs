namespace AGL.TPP.CustomerValidation.API.Providers.SapClient.Models
{
    public class SapPiResponseMessage
    {
        public string Type { get; set; }
        public string Id { get; set; }
        public string Number { get; set; }
        public string Description { get; set; }
        public string MessageV1 { get; set; }
        public string MessageV2 { get; set; }
        public string MessageV3 { get; set; }
        public string MessageV4 { get; set; }

        public static implicit operator SapError(SapPiResponseMessage error)
        {
            if (error == null) return null;

            return new SapError
            {
                Id = error.Id,
                Message = error.Description,
                Number = error.Number,
                Type = error.Type,
                MessageV1 = error.MessageV1 ?? string.Empty,
                MessageV2 = error.MessageV2 ?? string.Empty,
                MessageV3 = error.MessageV3 ?? string.Empty,
                MessageV4 = error.MessageV4 ?? string.Empty
            };
        }
    }
}
