using System;
using System.Collections.Generic;
using LBHAsbestosAPI.Models;
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
    }
}
