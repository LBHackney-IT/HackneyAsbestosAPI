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
        /// <param name="propertyId">A numeric string that identifies a property</param>
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
                if (!IdValidator.ValidatePropertyId(propertyId))
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
                string developerMessage;
                if (ex is InvalidLoginException)
                {
                    developerMessage = ex.Message;
                }
                else
                {
                    developerMessage = ex.StackTrace;
                }

                var userMessage = "We had some problems processing your request";

                var responseBuilder = new InspectionResponseBuilder();
                return responseBuilder.BuildErrorResponse(
                    userMessage, developerMessage, 500); 
            }
        }

        // GET properties
        /// <summary>
        /// Gets a room for a particular room id
        /// </summary>
        /// <param name="roomId">A numeric string that identifies a room</param>
        /// <returns>A room matching the specified room id</returns>
        /// <response code="200">Returns a room</response>
        /// <response code ="404">If the room id does not return any room</response>
        /// <response code="400">If the room id is not valid</response>   
        /// <response code="500">If any errors are encountered</response> 
        [HttpGet("room/{roomId}")]
        public async Task<JsonResult> GetRoom(string roomId)
        {
            try
            {
                var responseBuilder = new RoomResponseBuilder();
                _logger.LogInformation($"Calling RoomIdValidator() with {roomId}");
                if (!IdValidator.ValidateId(roomId))
                {
                    _logger.LogError("roomId has not passed validation");
                    var developerMessage = "Invalid parameter - roomId";
                    var userMessage = "Please provide a valid room id";

                    return responseBuilder.BuildErrorResponse(
                        userMessage, developerMessage, 400);
                }

                var _asbestosActions = new AsbestosActions(_asbestosService, _loggerActions);
                var response = await _asbestosActions.GetRoom(roomId);

                return responseBuilder.BuildSuccessResponse(response);
            }
            catch (MissingRoomException ex)
            {
                _logger.LogError("No room returned for roomId");
                var developerMessage = ex.Message;
                var userMessage = "Cannot find room";

                var responseBuilder = new RoomResponseBuilder();
                return responseBuilder.BuildErrorResponse(
                userMessage, developerMessage, 404);
            }
            catch (Exception ex)
            {
                string developerMessage;
                if (ex is InvalidLoginException)
                {
                    developerMessage = ex.Message;
                }
                else
                {
                    developerMessage = ex.StackTrace;
                }

                var userMessage = "We had some problems processing your request";

                var responseBuilder = new RoomResponseBuilder();
                return responseBuilder.BuildErrorResponse(
                    userMessage, developerMessage, 500);
            }
        }

        [HttpGet("floor/{floorId}")]
        public async Task<JsonResult> GetFloor(string floorId)
        {
            throw new NotImplementedException();
        }
    }
}
