namespace AGL.TPP.CustomerValidation.API.Providers.SapClient.Models
{
    public abstract class SapPiResponse<T>
    {
        public T Data { get; set; }
    }

    public abstract class SapPiResponse
    {
    }
}
