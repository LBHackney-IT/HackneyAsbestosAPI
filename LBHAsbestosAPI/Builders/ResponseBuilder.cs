using System;
using System.Collections.Generic;
using LBHAsbestosAPI.Models;
using LBHAsbestosAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace LBHAsbestosAPI.Builders
{
    public static class ResponseBuilder
    {
        public static JsonResult BuildSuccessResponse<T>(T response)
        {
            var successfulResult = new Dictionary<string, T>()
            {
                { "results", response }
            };

            return new JsonResult(successfulResult)
            {
                StatusCode = 200
            };
        }

        public static JsonResult BuildErrorResponse(string user, string developer, int errorCode)
        {
            var errorResult = new Dictionary<string, IEnumerable<ApiErrorMessage>>()
            {
                { "errors", new List<ApiErrorMessage>()
                    {
                        { new ApiErrorMessage()
                            {
                                developerMessage = developer,
                                userMessage = user
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

        public static JsonResult BuildErrorResponseFromException(Exception ex, string userMessage)
        {
            string developerMessage;

            if (ex is Psi2000ApiException)
            {
                developerMessage = ex.Message;
            }
            else
            {
                developerMessage = ex.StackTrace;
            }

            return BuildErrorResponse(userMessage, developerMessage, 500);
        }
    }
}
