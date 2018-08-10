using System.Collections.Generic;
using LBHAsbestosAPI.Entities;
using Microsoft.AspNetCore.Mvc;

namespace LBHAsbestosAPI.Builders
{
    public class DocumentResponseBuilder : ErrorResponseBuilder
    {
        public JsonResult BuildSuccessResponse(IEnumerable<Document> response)
        {
            var successResult = new Dictionary<string, IEnumerable<Document>>()
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
