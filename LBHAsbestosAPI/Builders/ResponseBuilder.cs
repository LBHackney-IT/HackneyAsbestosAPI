using System;
using System.Collections.Generic;
using LBHAsbestosAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace LBHAsbestosAPI.Builders
{
    public static class ResponseBuilder
    {
        public static JsonResult Ok(object response)
        {
            var successfulResult = new Dictionary<string, object>()
            {
                { "results", response }
            };

            return new JsonResult(successfulResult)
            {
                StatusCode = 200,
                ContentType = "application/json"
            };
        }

        public static JsonResult Error(int errorCode, string userMessage, string developerMessage)
        {
            var errorResult = new Dictionary<string, IEnumerable<ApiErrorMessage>>()
            {
                { "errors", new List<ApiErrorMessage>
                    {
                        { new ApiErrorMessage
                            {
                                DeveloperMessage = developerMessage,
                                UserMessage = userMessage
                            }
                        }
                    }
                }
            };

            return new JsonResult(errorResult)
            {
                StatusCode = errorCode
            };
        }
        //public static JsonResult BuildErrorResponseFromException(Exception ex, string userMessage)
        //{
        //    string developerMessage;

        //    if (ex is Psi2000ApiException)
        //    {
        //        developerMessage = ex.Message;
        //    }
        //    else
        //    {
        //        developerMessage = ex.StackTrace;
        //    }

        //    return Error(userMessage, developerMessage, 500);
        //}
    }
}
