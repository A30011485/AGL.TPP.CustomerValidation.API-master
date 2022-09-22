using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AGL.TPP.CustomerValidation.API.Models;
using AGL.TPP.CustomerValidation.API.Providers.SapClient.Models;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using Serilog;

namespace AGL.TPP.CustomerValidation.API.Services
{
    /// <summary>
    /// Service that maps the SAP error messages to friendly error messages
    /// </summary>
    public class SapErrorMessageService : ISapErrorMessageService
    {
        /// <summary>
        /// Logger instance
        /// </summary>
        private readonly ILogger _logger;

        /// <summary>
        /// Environment variable
        /// </summary>
        private readonly IHostingEnvironment _env;

        /// <summary>
        /// Sap error messages
        /// </summary>
        private List<SapErrorMessage> _sapErrorMessages;

        /// <summary>
        /// Initializes a new instance of <cref name="CustomerValidationService"></cref> class
        /// </summary>
        /// <param name="logger">Logger instance</param>
        /// <param name="salesOrderDataProvider">Sales order data provider</param>
        public SapErrorMessageService(
            IHostingEnvironment env,
            ILogger logger)
        {
            _logger = logger;
            _env = env;

            GetAllErrorMessages();
        }

        /// <summary>
        /// Gets error message based on code and message class
        /// </summary>
        /// <param name="code">Error code</param>
        /// <param name="messageClass">Message class</param>
        /// <returns>Returns Sap error message</returns>
        public SapErrorMessage GetApiErrorMessage(int sapCode, SapPiResponseMessage result)
        {
            return _sapErrorMessages.Where(x => x.Code == sapCode && x.Class == result.Id) //Retrieve API friendly message from local json using SAP Code and MessageClass
            .Select(localApiMessage => new SapErrorMessage
            {
                Message = localApiMessage.IsParamReplaceRequired ? ReplaceParamValue(localApiMessage.Message, result) : localApiMessage.Message, //Check if param replace is required. Replace API friendly message using SAP param values
                ApiCode = localApiMessage.ApiCode
            })
            .FirstOrDefault();
        }

        /// <summary>
        /// Reads SAP error messages from local json file
        /// </summary>
        private void GetAllErrorMessages()
        {
            var filePath = Path.Combine(_env.ContentRootPath, Constants.SapErrorMessagesFilePath);
            if (File.Exists(filePath))
            {
                var sapErrorMessagesJson = File.ReadAllText(filePath);
                if (!string.IsNullOrWhiteSpace(sapErrorMessagesJson))
                {
                    _sapErrorMessages = JsonConvert.DeserializeObject<List<SapErrorMessage>>(sapErrorMessagesJson);
                }
            }
        }

        private string ReplaceParamValue(string apiMessage, SapPiResponseMessage sapErrorObj)
        {
            try
            {
                apiMessage = ShieldMessage(apiMessage, sapErrorObj);
                apiMessage = apiMessage.Replace("&1", sapErrorObj.MessageV1 ?? "' '")
                          .Replace("&2", sapErrorObj.MessageV2 ?? "' '")
                          .Replace("&3", sapErrorObj.MessageV3 ?? "' '")
                          .Replace("&4", sapErrorObj.MessageV4 ?? "' '");
            }
            catch (Exception ex)
            {
                _logger.Error($"Error in ReplaceParamValue method:  {ex.Message}");
                return apiMessage;
            }

            return apiMessage;
        }

        private static string ShieldMessage(string message, SapPiResponseMessage sapPiResponseMessage)
        {
            if (string.IsNullOrWhiteSpace(message)) return message;
            if (!sapPiResponseMessage?.Number?.Equals("007") == true) return message;

            var fuelInfo = sapPiResponseMessage?.MessageV1 ?? sapPiResponseMessage?.MessageV2;

            var fuelReplaceValue = "";

            switch (fuelInfo)
            {
                case "01":
                    fuelReplaceValue = ConnectionType.Electricity.ToString();
                    break;

                case "02":
                    fuelReplaceValue = ConnectionType.Gas.ToString();
                    break;
            }

            return message.Replace("&12", fuelReplaceValue);
        }
    }
}