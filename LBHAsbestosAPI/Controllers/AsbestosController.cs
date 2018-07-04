using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LBHAsbestosAPI.Actions;
using LBHAsbestosAPI.Entities;
using LBHAsbestosAPI.Interfaces;
<<<<<<< Updated upstream
=======
using LBHAsbestosAPI.TempStuff;
using LBHAsbestosAPI.Validators;
>>>>>>> Stashed changes
using Microsoft.AspNetCore.Mvc;

namespace LBHAsbestosAPI.Controllers
{
	[Route("api/v1/")]
	public class AsbestosController : Controller
    {
		private IAsbestosService _asbestosService;

        public AsbestosController(IAsbestosService asbestosService)
        {
			_asbestosService = asbestosService;
        }
		
        [HttpGet("inspection/{propertyId}")]
<<<<<<< Updated upstream
        public async Task<JsonResult> GetInspection(string propertyId)    
		{
            var _asbestosActions = new AsbestosActions(_asbestosService);
            var response = await _asbestosActions.GetInspection(propertyId);

            // TODO example for returning an unsuccessful response
            //var resultsOutput = new Dictionary<string, ApiErrorMessage>()
            //{
            //    { "errors", new ApiErrorMessage()
            //        {
            //            userMessage = "XXX",
            //            developerMessage = "XXX"
            //        }}
            //};
=======
        public async Task<JsonResult> GetInspection(string propertyId)
        {
            try
            {
                var responseBuilder = new InspectionResponseBuilder();

                if (!InspectionIdValidator.Validate(propertyId))
                {
                    var developerMessage = "Invalid parameter - propertyId";
                    var userMessage = "Please provide a valid property id";

                    return responseBuilder.BuildErrorResponse(
                        userMessage, developerMessage, 400);
                }

                var _asbestosActions = new AsbestosActions(_asbestosService);
                var response = await _asbestosActions.GetInspection(propertyId);
>>>>>>> Stashed changes

                return responseBuilder.BuildSuccessResponse(response);
            }
            catch (MissingInspectionException ex)
            {
                var developerMessage = ex.Message;
                var userMessage = "Cannot find inspection";

                var responseBuilder = new InspectionResponseBuilder();
                return responseBuilder.BuildErrorResponse(
                userMessage, developerMessage, 404);
            }
            catch (Exception ex)
            {
<<<<<<< Updated upstream
                StatusCode = 200
            };
		}
	}
=======
                var developerMessage = ex.Message;
                var userMessage = "We had some problems processing your request";

                var responseBuilder = new InspectionResponseBuilder();
                return responseBuilder.BuildErrorResponse(
                    userMessage, developerMessage, 500);
            }
        }
    }
>>>>>>> Stashed changes
}
