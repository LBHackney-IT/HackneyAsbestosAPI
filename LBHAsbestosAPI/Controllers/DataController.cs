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
  
        // GET inspections
        /// <summary>
        /// Returns a list of inspections
        /// </summary>
        /// <param name="propertyId">Universal Housing property Id</param>
        /// <returns>A list of inspections</returns>
        /// <response code="200">Returns a list of inspections</response>
        /// <response code="404">No inspections found</response>
        /// <response code="400">Property id is not valid</response>   
        /// <response code="500">Internal server error</response>  
        [HttpGet("inspections")]
        public async Task<JsonResult> GetInspection(string propertyId)
		{ 
            try
            {
                _logger.LogInformation($"Calling ValidatePropertyId() with {propertyId}");
                if (propertyId == null)
                {
                    var developerMessage = $"Missing parameter - propertyId";
                    var userMessage = "Please provide a valid property id";

                    return ResponseBuilder.BuildErrorResponse(
                        userMessage, developerMessage, 400);
                }
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

        // GET room
        /// <summary>
        /// Returns room information
        /// </summary>
        /// <param name="roomId">PSI2000 room id</param>
        /// <returns>Returns room information</returns>
        /// <response code="200">Returns a room</response>
        /// <response code="404">No rooms found</response>
        /// <response code="400">Room id is not valid</response>   
        /// <response code="500">Internal server error</response> 
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

        // GET floor
        /// <summary>
        /// Returns floor information
        /// </summary>
        /// <param name="floorId">PSI2000 floor id</param>
        /// <returns>Returns floor information</returns>
        /// <response code="200">Returns a floor</response>
        /// <response code="404">No floor found</response>
        /// <response code="400">floor id is not valid</response>   
        /// <response code="500">Internal server error</response> 
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

        // GET element
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

        // GET todo
        /// <summary>
        /// Returns todo information
        /// </summary>
        /// <param name="todoId">PSI2000 todo id</param>
        /// <returns>Returns todo information</returns>
        /// <response code="200">Returns a todo</response>
        /// <response code="404">No todos found</response>
        /// <response code="400">todo id is not valid</response>   
        /// <response code="500">Internal server error</response> 
        [HttpGet("todos/{todoId}")]
        public async Task<JsonResult> GetTodo(string todoId)
        {
            try
            {
                _logger.LogInformation($"Calling ValidateId() with {todoId}");

                if (!IdValidator.ValidateId(todoId))
                {
                    _logger.LogError("todoID has not passed validation");
                    var developerMessage = "Invalid parameter - todoId";
                    var userMessage = "Please provide a valid todo id";

                    return ResponseBuilder.BuildErrorResponse(
                        userMessage, developerMessage, 400);
                }

                var _asbestosActions = new AsbestosActions(_asbestosService, _loggerActions);
                var response = await _asbestosActions.GetTodo(todoId);

                return ResponseBuilder.BuildSuccessResponse(response);
            }
            catch (MissingTodoException ex)
            {
                _logger.LogError("No todo returned for todoId");
                var developerMessage = ex.Message;
                var userMessage = "Cannot find todo";

                return ResponseBuilder.BuildErrorResponse(
                userMessage, developerMessage, 404);
            }
            catch (Exception ex)
            {
                var userMessage = "We had some problems processing your request";
                return ResponseBuilder.BuildErrorResponseFromException(ex, userMessage);
            }
        }

        // GET todos by propertyId
        /// <summary>
        /// Returns a list of todos
        /// </summary>
        /// <param name="propertyId">Universal Housing property Id</param>
        /// <returns>A list of todos</returns>
        /// <response code="200">Returns a list of todos</response>
        /// <response code="404">No todos found</response>
        /// <response code="400">Property id is not valid</response>   
        /// <response code="500">Internal server error</response>
        [HttpGet("todos")]  
        public async Task<JsonResult> GetTodosByPropertyId(string propertyId)
        {
            try
            {
                _logger.LogInformation($"Calling ValidatePropertyId() with {propertyId}");
                if (propertyId == null)
                {
                    var developerMessage = $"Missing parameter - propertyId";
                    var userMessage = "Please provide a valid property id";

                    return ResponseBuilder.BuildErrorResponse(
                        userMessage, developerMessage, 400);
                }
                if (!IdValidator.ValidatePropertyId(propertyId))
                {
                    _logger.LogError("propertyId has not passed validation");
                    var developerMessage = "Invalid parameter - propertyId";
                    var userMessage = "Please provide a valid property id";

                    return ResponseBuilder.BuildErrorResponse(
                        userMessage, developerMessage, 400);
                }

                var _asbestosActions = new AsbestosActions(_asbestosService, _loggerActions);
                var response = await _asbestosActions.GetTodosByPropertyId(propertyId);

                return ResponseBuilder.BuildSuccessResponse(response);
            }
            catch (MissingTodoException ex)
            {
                _logger.LogError("No todos returned for propertyId");
                var developerMessage = ex.Message;
                var userMessage = "Cannot find todos";

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
