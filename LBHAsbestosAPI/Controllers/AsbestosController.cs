using System;
using System.Threading.Tasks;
using LBHAsbestosAPI.Actions;
using LBHAsbestosAPI.Interfaces;
using LBHAsbestosAPI.TempStuff;
using LBHAsbestosAPI.Validators;
using Microsoft.AspNetCore.Mvc;

namespace LBHAsbestosAPI.Controllers
{
	[Route("api/v1/")]
	public class AsbestosController : Controller
    {
		IAsbestosService _asbestosService;

        public AsbestosController(IAsbestosService asbestosService)
        {
			_asbestosService = asbestosService;
        }
		
        [HttpGet("inspection/{propertyId}")]
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
                var developerMessage = ex.Message;
                var userMessage = "We had some problems processing your request";

                var responseBuilder = new InspectionResponseBuilder();
                return responseBuilder.BuildErrorResponse(
                    userMessage, developerMessage, 500); 
            }
        }
    }
}
