using System;
using System.Collections.Generic;
using LBHAsbestosAPI.Entities;
using Microsoft.AspNetCore.Mvc;

namespace LBHAsbestosAPI.Builders
{
    public class RoomResponseBuilder : ErrorResponseBuilder
    {
        public JsonResult BuildSuccessResponse(Room response)
        {
            var successResult = new Dictionary<string, Room>()
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
