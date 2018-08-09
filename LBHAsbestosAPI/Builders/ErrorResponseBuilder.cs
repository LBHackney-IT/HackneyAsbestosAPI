using System;
using System.Collections.Generic;
using LBHAsbestosAPI.Models;
using LBHAsbestosAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace LBHAsbestosAPI.Builders
{
    public class ErrorResponseBuilder
    {
        public JsonResult BuildErrorResponse(string user, string developer, int errorCode)
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

        public JsonResult BuildErrorResponseFromException(Exception ex, string userMessage)
        {
            string developerMessage;

            if (ex is InvalidLoginException)
            {
                developerMessage = ex.Message;
            }
            else
            {
                developerMessage = ex.StackTrace;
            }

            var responseBuilder = new ErrorResponseBuilder();
            return responseBuilder.BuildErrorResponse(
                    userMessage, developerMessage, 500);
        }
    }
}
