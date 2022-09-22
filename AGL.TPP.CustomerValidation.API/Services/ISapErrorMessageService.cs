using AGL.TPP.CustomerValidation.API.Models;
using AGL.TPP.CustomerValidation.API.Providers.SapClient.Models;

namespace AGL.TPP.CustomerValidation.API.Services
{
    /// <summary>
    /// Interface that maps the SAP error messages to friendly error messages
    /// </summary>
    public interface ISapErrorMessageService
    {
        SapErrorMessage GetApiErrorMessage(int code, SapPiResponseMessage result);
    }
}