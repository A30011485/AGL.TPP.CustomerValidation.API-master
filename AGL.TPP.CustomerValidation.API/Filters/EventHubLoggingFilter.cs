using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using AGL.TPP.CustomerValidation.API.Extensions.Web;
using AGL.TPP.CustomerValidation.API.Models;
using AGL.TPP.CustomerValidation.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace AGL.TPP.CustomerValidation.API.Filters
{
    /// <summary>
    /// Action filter for Event Hub Logging
    /// </summary>
    public class EventHubLoggingFilter : IActionFilter
    {
        /// <summary>
        /// Before the action is executed
        /// </summary>
        /// <param name="context"></param>
        public void OnActionExecuted(ActionExecutedContext context)
        {
            CustomerValidationResponse result = null;
            string messageType = string.Empty;

            if (context.Result is OkObjectResult okResponse)
            {
                messageType = Constants.ThirdPartyApiSuccess;
                result = okResponse.Value as CustomerValidationResponse;
            }
            else if (context.Result is BadRequestObjectResult badRequestResponse)
            {
                messageType = Constants.ThirdPartyApiError;
                result = badRequestResponse.Value as CustomerValidationResponse;
            }

            if (result != null)
            {
                var eventHubLoggingService =
                    context.HttpContext.RequestServices.GetService(typeof(IEventHubLoggingService))
                    as EventHubLoggingService;

                var headerInfo = ExtractHeaderFromRequest(context.HttpContext.Request);

                var correlationId = context.HttpContext.Request?.GetRequestHeaderOrDefault(Constants.CorrelationKey, $"GEN-{Guid.NewGuid().ToString()}");
                var apiName = context.HttpContext.Request?.GetRequestHeaderOrDefault("Api-Name", EnvironmentConstants.EndpointSourceCustomerValidation);

                var updatedResponse = new
                {
                    MessageType = messageType,
                    Timestamp = GetTimestamp(),
                    MessageBody = new
                    {
                        Header = new
                        {
                            headerInfo?.Header?.VendorName,
                            headerInfo?.Header?.VendorBusinessPartnerNumber,
                            headerInfo?.Header?.VendorLeadId,
                            CorrelationId = correlationId,
                            headerInfo?.Header?.TransactionType,
                            ApiName = apiName
                        },
                        Payload = new
                        {
                            result.Code,
                            result.Message,
                            result.Errors
                        }
                    }
                };

                eventHubLoggingService?.Send(JsonSerialize(updatedResponse), updatedResponse);
            }
        }

        private ResidentialCustomerSalesValidationModel ExtractHeaderFromRequest(HttpRequest httpContextRequest)
        {
            string requestBody;
            httpContextRequest.HttpContext.Request.EnableRewind();
            using (var stream = new StreamReader(httpContextRequest.HttpContext.Request.Body))
            {
                stream.BaseStream.Position = 0;
                requestBody = stream.ReadToEnd();
            }

            if (string.IsNullOrWhiteSpace(requestBody)) return null;
            var validationModel = JsonConvert.DeserializeObject<ResidentialCustomerSalesValidationModel>(requestBody);
            return validationModel;
        }

        /// <summary>
        /// While the action is executing
        /// </summary>
        /// <param name="context"></param>
        public void OnActionExecuting(ActionExecutingContext context)
        {
            var eventHubLoggingService =
                context.HttpContext.RequestServices.GetService(typeof(IEventHubLoggingService))
                as EventHubLoggingService;

            var model = context.ActionArguments.FirstOrDefault();

            var correlationId = context.HttpContext.Request?.GetRequestHeaderOrDefault(Constants.CorrelationKey, $"GEN-{Guid.NewGuid().ToString()}");

            var apiName = context.HttpContext.Request?.GetRequestHeaderOrDefault("Api-Name", EnvironmentConstants.EndpointSourceCustomerValidation);

            var messageType = GetMessageType(model.Value);
            if (string.IsNullOrEmpty(messageType))
            {
                var routeData = context.RouteData?.Values?.Values?.ToList();
                if (routeData != null && routeData.Count == 2)
                {
                    messageType = GetMessageTypeFromRouteData(routeData);
                }
            }
            var updatedResponse = new
            {
                MessageType = messageType,
                Timestamp = GetTimestamp(),
                MessageBody = GetResponse(model.Value, correlationId, apiName)
            };

            if (model.Value != null)
            {
                eventHubLoggingService?.Send(JsonSerialize(updatedResponse), updatedResponse);
            }
        }

        private dynamic GetResponse(object value, string correlationId, string apiName)
        {
            dynamic dynamicModel = value;

            if (dynamicModel == null || dynamicModel.Header == null || dynamicModel?.Payload == null) return null;

            dynamic response = new ExpandoObject();
            response.Header = new Dictionary<string, string>();
            response.Header.Add("CorrelationId", correlationId);
            response.Header.Add("ApiName", apiName);
            response.Payload = dynamicModel?.Payload;

            if (dynamicModel != null)
            {
                foreach (var property in dynamicModel.Header.GetType().GetProperties())
                {
                    response.Header.Add(property.Name, property.GetValue(dynamicModel.Header));
                }
            }

            return response;
        }

        /// <summary>
        /// Serializes the model to json
        /// </summary>
        /// <param name="model">Input object model</param>
        /// <returns>Returns Json string</returns>
        private string JsonSerialize(object model)
        {
            var settings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            return JsonConvert.SerializeObject(model, Formatting.Indented, settings);
        }

        private string GetMessageType(object model)
        {
            string messageType = null;
            if (model != null)
            {
                var header = model.GetType().GetProperty("Header")?.GetValue(model);
                var body = model.GetType().GetProperty("Payload")?.GetValue(model);

                if (header == null || body == null)
                {
                    return string.Empty;
                }

                var transactionType = (string)header.GetType().GetProperty("TransactionType")?.GetValue(header);
                var customerType = body.GetType().ToString().Contains("Residential") ? "Res" : "Bus";

                switch (customerType)
                {
                    case "Res":
                        switch (transactionType?.ToLowerInvariant())
                        {
                            case "sale":
                                messageType = Constants.ThirdPartyResidentialSale;
                                break;

                            case "sdfi":
                                messageType = Constants.ThirdPartyResidentialSdfi;
                                break;

                            case "cancel":
                                messageType = Constants.ThirdPartyResidentialCancel;
                                break;

                            case "moveout":
                                messageType = Constants.ThirdPartyResidentialMoveout;
                                break;

                            case "change":
                                messageType = Constants.ThirdPartyResidentialChange;
                                break;
                        }
                        break;

                    case "Bus":
                        switch (transactionType?.ToLowerInvariant())
                        {
                            case "sale":
                                messageType = Constants.ThirdPartyBusinessSale;
                                break;

                            case "sdfi":
                                messageType = Constants.ThirdPartyBusinessSdfi;
                                break;

                            case "cancel":
                                messageType = Constants.ThirdPartyBusinessCancel;
                                break;

                            case "moveout":
                                messageType = Constants.ThirdPartyBusinessMoveout;
                                break;

                            case "change":
                                messageType = Constants.ThirdPartyBusinessChange;
                                break;
                        }
                        break;
                }
            }

            return messageType;
        }

        private string GetMessageTypeFromRouteData(IReadOnlyList<object> routeData)
        {
            if (routeData[0].ToString().Contains("Residential"))
                switch (routeData[1])
                {
                    case "CustomerSales":
                        return Constants.ThirdPartyResidentialSale;

                    case "CustomerSalesCancellation":
                        return Constants.ThirdPartyResidentialCancel;

                    case "CustomerMoveOut":
                        return Constants.ThirdPartyResidentialMoveout;

                    case "CustomerChange":
                        return Constants.ThirdPartyResidentialChange;
                }
            else if (routeData[0].ToString().Contains("Business"))
                switch (routeData[1])
                {
                    case "CustomerSales":
                        return Constants.ThirdPartyBusinessSale;

                    case "CustomerSalesCancellation":
                        return Constants.ThirdPartyBusinessCancel;

                    case "CustomerMoveOut":
                        return Constants.ThirdPartyBusinessMoveout;

                    case "CustomerChange":
                        return Constants.ThirdPartyBusinessChange;
                }

            return null;
        }

        private string GetTimestamp()
        {
            return DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss");
        }
    }
}
