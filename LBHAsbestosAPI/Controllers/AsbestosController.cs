using System;
using System.Threading.Tasks;
using LBHAsbestosAPI.Actions;
using LBHAsbestosAPI.Interfaces;
using LBHAsbestosAPI.Builders;
using LBHAsbestosAPI.Validators;
using Microsoft.AspNetCore.Mvc;

namespace LBHAsbestosAPI.Controllers
{
	[Route("api/v1/")]
	public class AsbestosController : Controller
    {
		IAsbestosService _asbestosService;
        ILoggerAdapter<AsbestosActions> _loggerActions;
        protected readonly ILoggerAdapter<AsbestosController> _logger;

        public AsbestosController(IAsbestosService asbestosService, ILoggerAdapter<AsbestosController> logger,
                                  ILoggerAdapter<AsbestosActions> loggerActions)
        {
			_asbestosService = asbestosService;
            _logger = logger;
            _loggerActions = loggerActions;
        }
  
        // GET properties
        /// <summary>
        /// Gets a list of inspections for a particular property id
        /// </summary>
        /// <param name="propertyId">An 8 digit number that identifies a property</param>
        /// <returns>A list of inspections matching the specified property id</returns>
        /// <response code="200">Returns the list of inspections</response>
        /// <response code ="404">If the property id does not return any inspections</response>
        /// <response code="400">If the inspection id is not valid</response>   
        /// <response code="500">If any errors are encountered</response>  	
        [HttpGet("inspection/{propertyId}")]
        public async Task<JsonResult> GetInspection(string propertyId)
		{ 
            try
            {
                var responseBuilder = new InspectionResponseBuilder();
                _logger.LogInformation($"Calling InspectionIdValidator() with {propertyId}");
                if (!InspectionIdValidator.Validate(propertyId))
                {
                    _logger.LogError("propertyId has not passed validation");
                    var developerMessage = "Invalid parameter - inspectionId";
                    var userMessage = "Please provide a valid inspection id";

                    return responseBuilder.BuildErrorResponse(
                        userMessage, developerMessage, 400);
                }

                var _asbestosActions = new AsbestosActions(_asbestosService, _loggerActions);
                var response = await _asbestosActions.GetInspection(propertyId);

                return responseBuilder.BuildSuccessResponse(response);
            }
            catch (MissingInspectionException ex)
            {
                _logger.LogError("No inspections returned for propertyId");
                var developerMessage = ex.Message;
                var userMessage = "Cannot find inspection";

                var responseBuilder = new InspectionResponseBuilder();
                return responseBuilder.BuildErrorResponse(
                userMessage, developerMessage, 404);
            }
            catch (Exception ex)
            {
                var developerMessage = ex.StackTrace;
                var userMessage = "We had some problems processing your request";

                var responseBuilder = new InspectionResponseBuilder();
                return responseBuilder.BuildErrorResponse(
                    userMessage, developerMessage, 500); 
            }
        }
    }
}
