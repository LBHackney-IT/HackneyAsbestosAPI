using System;
using System.Collections.Generic;
using LBHAsbestosAPI.Entities;
using Microsoft.AspNetCore.Mvc;

namespace LBHAsbestosAPI.Builders
{
    public class InspectionResponseBuilder : ErrorResponseBuilder
    {
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
