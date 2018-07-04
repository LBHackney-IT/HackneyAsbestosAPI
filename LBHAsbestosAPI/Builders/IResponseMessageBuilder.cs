using System;
using System.Collections.Generic;
using LBHAsbestosAPI.Entities;
using Microsoft.AspNetCore.Mvc;

namespace LBHAsbestosAPI.TempStuff
{
    public interface IResponseBuilder
    {
        JsonResult BuildErrorResponse(string userMessage, string developerMessage, int errorCode);
        JsonResult BuildSuccessResponse(IEnumerable<Inspection> response);
    }
}
