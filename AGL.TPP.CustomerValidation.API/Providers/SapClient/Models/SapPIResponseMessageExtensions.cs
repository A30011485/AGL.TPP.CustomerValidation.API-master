using System;

namespace AGL.TPP.CustomerValidation.API.Providers.SapClient.Models
{
    internal static class SapPiResponseMessageExtensions
    {
        internal static bool IsSuccess(this SapPiResponseMessage message)
        {
            return "success".Equals(message?.Type, StringComparison.InvariantCultureIgnoreCase) ||
                   "s".Equals(message?.Type, StringComparison.InvariantCultureIgnoreCase);
        }

        internal static bool IsSuccess(this SapPiResponseMessage message, string type)
        {
            return "success".Equals(type, StringComparison.InvariantCultureIgnoreCase) ||
                   "s".Equals(type, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}
