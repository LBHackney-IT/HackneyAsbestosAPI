using System;
using System.Collections.Generic;
using LBHAsbestosAPI.Entities;
using Microsoft.AspNetCore.Mvc;

namespace LBHAsbestosAPI.Builders
{
    public class FloorResponseBuilder : ErrorResponseBuilder
    {
        public JsonResult BuildSuccessResponse(Floor response)
        {
            var successResult = new Dictionary<string, Floor>()
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
