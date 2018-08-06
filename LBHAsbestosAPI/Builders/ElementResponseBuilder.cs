using System;
using System.Collections.Generic;
using LBHAsbestosAPI.Entities;
using Microsoft.AspNetCore.Mvc;

namespace LBHAsbestosAPI.Builders
{
    public class ElementResponseBuilder : ErrorResponseBuilder
    {
        public JsonResult BuildSuccessResponse(Element response)
        {
            var successResult = new Dictionary<string, Element>()
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

