namespace AGL.TPP.CustomerValidation.API.Providers.SapClient.Models
{
    public class SapPiSingleMessageResponse<T> : SapPiResponse<T>
    {
        public bool IsSuccess => Message.IsSuccess();
        public SapPiResponseMessage Message { get; set; }
    }

    public class SapPiSingleMessageResponse : SapPiResponse
    {
        public bool IsSuccess => Message.IsSuccess();
        public SapPiResponseMessage Message { get; set; }
    }
}
