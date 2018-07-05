using System;
using System.Collections.Generic;
using LBHAsbestosAPI.Entities;
using Microsoft.AspNetCore.Mvc;

namespace LBHAsbestosAPI.Builders
{
    public class InspectionResponseBuilder : ErrorResponseBuilder
    {
        //public JsonResult BuildErrorResponse(string user, string developer, int errorCode)
        //{
        //    var errorResult = new Dictionary<string, IEnumerable<ApiErrorMessage>>()
        //    {
        //        { "errors", new List<ApiErrorMessage>()
        //            {
        //                { new ApiErrorMessage()
        //                    {
        //                        developerMessage = developer,
        //                        userMessage = user
        //                    }
        //                }
        //            }
        //        }
        //    };

        //    return new JsonResult(errorResult)
        //    {
        //        StatusCode = errorCode
        //    };
        //}

        public JsonResult BuildSuccessResponse(IEnumerable<Inspection> response)
        {
            var successResult = new Dictionary<string, IEnumerable<Inspection>>()
            {
                { "results", response }
            };

            return new JsonResult(successResult)
            {
                StatusCode = 200
            };
        }
    }
}
