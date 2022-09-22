namespace AGL.TPP.CustomerValidation.API.Providers.SapClient.Models
{
    public class SapError
    {
        public string Id { get; set; }
        public string Type { get; set; }

        public string Number { get; set; }

        public string Message { get; set; }

        public string MessageV1 { get; set; }
        public string MessageV2 { get; set; }
        public string MessageV3 { get; set; }
        public string MessageV4 { get; set; }

        public static implicit operator InternalError(SapError error)
        {
            if (error == null)
                return null;
            return new InternalError
            {
                Description = error.Message,
                ErrorId = error.Id,
                ErrorNumber = error.Number
            };
        }
    }
}
