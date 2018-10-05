using System;
using System.Threading.Tasks;
using LBHAsbestosAPI.Actions;
using LBHAsbestosAPI.Interfaces;
using LBHAsbestosAPI.Builders;
using LBHAsbestosAPI.Validators;
using Microsoft.AspNetCore.Mvc;
using LBHAsbestosAPI.Repositories;

namespace LBHAsbestosAPI.Controllers
{
	[Route("api/v1/")]
	public class DataController : Controller
    {
	    IAsbestosService _asbestosService;
        ILoggerAdapter<AsbestosActions> _loggerActions;
        protected readonly ILoggerAdapter<DataController> _logger;

        public DataController(IAsbestosService asbestosService, ILoggerAdapter<DataController> logger,
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
        /// <param name="propertyId">A string that identifies a property</param>
        /// <returns>A list of inspections matching the specified property id</returns>
        /// <response code="200">Returns the list of inspections</response>
        /// <response code="404">If the property id does not return any inspections</response>
        /// <response code="400">If the inspection id is not valid</response>   
        /// <response code="500">If any errors are encountered</response>  	
        [HttpGet("inspections")]
        public async Task<JsonResult> GetInspection(string propertyId)
		{ 
            try
            {
                _logger.LogInformation($"Calling ValidatePropertyId() with {propertyId}");

                if (!IdValidator.ValidatePropertyId(propertyId))
                {
                    _logger.LogError("propertyId has not passed validation");
                    var developerMessage = "Invalid parameter - propertyId";
                    var userMessage = "Please provide a valid property id";

                    return ResponseBuilder.BuildErrorResponse(
                        userMessage, developerMessage, 400);
                }

                var _asbestosActions = new AsbestosActions(_asbestosService, _loggerActions);
                var response = await _asbestosActions.GetInspection(propertyId);

                return ResponseBuilder.BuildSuccessResponse(response);
            }
            catch (MissingInspectionException ex)
            {
                _logger.LogError("No inspections returned for propertyId");
                var developerMessage = ex.Message;
                var userMessage = "Cannot find inspection";

                return ResponseBuilder.BuildErrorResponse(
                userMessage, developerMessage, 404);
            }
            catch (Exception ex)
            {
                var userMessage = "We had some problems processing your request";
                return ResponseBuilder.BuildErrorResponseFromException(ex, userMessage);
            }
        }

        // GET properties
        /// <summary>
        /// Gets a room for a particular room id
        /// </summary>
        /// <param name="roomId">A string that identifies a room</param>
        /// <returns>A room matching the specified room id</returns>
        /// <response code="200">Returns a room</response>
        /// <response code="404">If the room id does not return any room</response>
        /// <response code="400">If the room id is not valid</response>   
        /// <response code="500">If any errors are encountered</response> 
        [HttpGet("rooms/{roomId}")]
        public async Task<JsonResult> GetRoom(string roomId)
        {
            try
            {
                _logger.LogInformation($"Calling ValidateId() with {roomId}");

                if (!IdValidator.ValidateId(roomId))
                {
                    _logger.LogError("roomId has not passed validation");
                    var developerMessage = "Invalid parameter - roomId";
                    var userMessage = "Please provide a valid room id";

                    return ResponseBuilder.BuildErrorResponse(
                        userMessage, developerMessage, 400);
                }

                var _asbestosActions = new AsbestosActions(_asbestosService, _loggerActions);
                var response = await _asbestosActions.GetRoom(roomId);

                return ResponseBuilder.BuildSuccessResponse(response);
            }
            catch (MissingRoomException ex)
            {
                _logger.LogError("No room returned for roomId");
                var developerMessage = ex.Message;
                var userMessage = "Cannot find room";

                return ResponseBuilder.BuildErrorResponse(
                userMessage, developerMessage, 404);
            }
            catch (Exception ex)
            {
                var userMessage = "We had some problems processing your request";
                return ResponseBuilder.BuildErrorResponseFromException(ex, userMessage);
            }
        }

        // GET properties
        /// <summary>
        /// Gets a floor for a particular floor id
        /// </summary>
        /// <param name="floorId">A string that identifies a floor</param>
        /// <returns>A floor matching the specified floor id</returns>
        /// <response code="200">Returns a floor</response>
        /// <response code="404">If the floor id does not return any floor</response>
        /// <response code="400">If the floor id is not valid</response>   
        /// <response code="500">If any errors are encountered</response> 
        [HttpGet("floors/{floorId}")]
        public async Task<JsonResult> GetFloor(string floorId)
        {
            try
            {
                _logger.LogInformation($"Calling ValidateId() with {floorId}");

                if (!IdValidator.ValidateId(floorId))
                {
                    _logger.LogError("floorId has not passed validation");
                    var developerMessage = "Invalid parameter - floorId";
                    var userMessage = "Please provide a valid floor id";

                    return ResponseBuilder.BuildErrorResponse(
                        userMessage, developerMessage, 400);
                }

                var _asbestosActions = new AsbestosActions(_asbestosService, _loggerActions);
                var response = await _asbestosActions.GetFloor(floorId);

                return ResponseBuilder.BuildSuccessResponse(response);
            }
            catch (MissingFloorException ex)
            {
                _logger.LogError("No floor returned for floorId");
                var developerMessage = ex.Message;
                var userMessage = "Cannot find floor";

                return ResponseBuilder.BuildErrorResponse(
                userMessage, developerMessage, 404);
            }
            catch (Exception ex)
            {
                var userMessage = "We had some problems processing your request";
                return ResponseBuilder.BuildErrorResponseFromException(ex, userMessage);
            }
        }

        // GET properties
        /// <summary>
        /// Gets an element for a particular element id
        /// </summary>
        /// <param name="elementId">A string that identifies an element</param>
        /// <returns>An element matching the specified element id</returns>
        /// <response code="200">Returns an element</response>
        /// <response code="404">If the element id does not return any element</response>
        /// <response code="400">If the element id is not valid</response>   
        /// <response code="500">If any errors are encountered</response> 
        [HttpGet("elements/{elementId}")]
        public async Task<JsonResult> GetElement(string elementId)
        {
            try
            {
                _logger.LogInformation($"Calling ValidateId() with {elementId}");

                if (!IdValidator.ValidateId(elementId))
                {
                    _logger.LogError("elementId has not passed validation");
                    var developerMessage = "Invalid parameter - elementId";
                    var userMessage = "Please provide a valid element id";

                    return ResponseBuilder.BuildErrorResponse(
                        userMessage, developerMessage, 400);
                }

                var _asbestosActions = new AsbestosActions(_asbestosService, _loggerActions);
                var response = await _asbestosActions.GetElement(elementId);

                return ResponseBuilder.BuildSuccessResponse(response);
            }
            catch (MissingElementException ex)
            {
                _logger.LogError("No element returned for elementId");
                var developerMessage = ex.Message;
                var userMessage = "Cannot find element";

                return ResponseBuilder.BuildErrorResponse(
                userMessage, developerMessage, 404);
            }
            catch (Exception ex)
            {
                var userMessage = "We had some problems processing your request";
                return ResponseBuilder.BuildErrorResponseFromException(ex, userMessage);
            }
        }
    }
}
