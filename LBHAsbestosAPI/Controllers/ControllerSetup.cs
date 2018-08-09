using System;
using System.Diagnostics.Contracts;
using LBHAsbestosAPI.Actions;
using LBHAsbestosAPI.Builders;
using LBHAsbestosAPI.Interfaces;
using LBHAsbestosAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace LBHAsbestosAPI.Controllers
{
    public class ControllerSetup : Controller
    {
        IAsbestosService _asbestosService;
        ILoggerAdapter<AsbestosActions> _loggerActions;
        protected readonly ILoggerAdapter<DataController> _logger;

        public ControllerSetup(IAsbestosService asbestosService, ILoggerAdapter<DataController> logger,
                               ILoggerAdapter<AsbestosActions> loggerActions) : base
        {
            _asbestosService = asbestosService;
            _logger = logger;
            _loggerActions = loggerActions;
        }

        private JsonResult BuildErrorResponseFromException(Exception ex)
        {
            Contract.Ensures(Contract.Result<JsonResult>() != null);
            var userMessage = "We had some problems processing your request";
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
