using System;
using System.Net;
using System.Threading.Tasks;
using LBHAsbestosAPI.Actions;
using LBHAsbestosAPI.Builders;
using LBHAsbestosAPI.Interfaces;
using LBHAsbestosAPI.Validators;
using Microsoft.AspNetCore.Mvc;

namespace LBHAsbestosAPI.Controllers
{
    [Route("api/v1/document")]
    public class DocumentController : Controller
    {
        IAsbestosService _asbestosService;
        ILoggerAdapter<AsbestosActions> _loggerActions;
        protected readonly ILoggerAdapter<DocumentController> _logger;

        public DocumentController(IAsbestosService asbestosService, ILoggerAdapter<DocumentController> logger,
                                  ILoggerAdapter<AsbestosActions> loggerActions)
        {
            _asbestosService = asbestosService;
            _logger = logger;
            _loggerActions = loggerActions;
        }

        // GET properties
        /// <summary>
        /// Gets an photo for a particular photo id
        /// </summary>
        /// <param name="photoId">A string that identifies an photo</param>
        /// <returns>A photo matching the specified photo id</returns>
        /// <response code="200">Returns a photo</response>
        /// <response code="404">If the photo id does not return any result</response>
        /// <response code="400">If the photo id is not valid</response>   
        /// <response code="500">If any errors are encountered</response> 
        [HttpGet("photo/{photoId:required}")]
        public async Task<IActionResult> GetPhoto(string photoId)
        {
            if (!IdValidator.ValidateId(photoId))
            {
                var developerMessage = $"Invalid parameter - photoId";
                var userMessage = "Please provide a valid photo id";

                return ResponseBuilder.BuildErrorResponse(
                    userMessage, developerMessage, (int)HttpStatusCode.BadRequest);
            }

            try
            {
                var asbestosActions = new AsbestosActions(_asbestosService, _loggerActions);
                var response = await asbestosActions.GetPhoto(photoId);
                return File(response.Data, response.ContentType);
            }
            catch (MissingFileException ex)
            {
                var developerMessage = ex.Message;
                var userMessage = "Cannot find file";

                return ResponseBuilder.BuildErrorResponse(
                    userMessage, developerMessage, (int)HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                var userMessage = "We had some problems processing your request";
                return ResponseBuilder.BuildErrorResponseFromException(
                    ex, userMessage);
            }
        }

        // GET properties
        /// <summary>
        /// Gets a list of documents related to a photo for a propertyId
        /// </summary>
        /// <param name="propertyId">A string that identifies an property</param>
        /// <returns>A list of documents related to a photo</returns>
        /// <response code="200">Returns a list of documents</response>
        /// <response code="404">If the property id does not return any result</response>
        /// <response code="400">If the property id is not valid</response>   
        /// <response code="500">If any errors are encountered</response> 
        [HttpGet("photo")]
        public async Task<JsonResult> GetPhotoDocuments(string propertyId)
        {
            if (propertyId == null)
            {
                var developerMessage = $"Missing parameter - propertyId";
                var userMessage = "Please provide a valid property id";

                return ResponseBuilder.BuildErrorResponse(
                    userMessage, developerMessage, (int)HttpStatusCode.BadRequest);
            }
            if (!IdValidator.ValidatePropertyId(propertyId))
            {
                var developerMessage = $"Invalid parameter - propertyId";
                var userMessage = "Please provide a valid property id";

                return ResponseBuilder.BuildErrorResponse(
                    userMessage, developerMessage, (int)HttpStatusCode.BadRequest);
            }

            try
            {
                var asbestosActions = new AsbestosActions(_asbestosService, _loggerActions);
                var response = await asbestosActions.GetPhotoDocuments(propertyId);
                return ResponseBuilder.BuildSuccessResponse(response);
            }
            catch (MissingDocumentException ex)
            {
                var developerMessage = ex.Message;
                var userMessage = "Cannot find document";

                return ResponseBuilder.BuildErrorResponse(
                    userMessage, developerMessage, (int)HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                var userMessage = "We had some problems processing your request";
                return ResponseBuilder.BuildErrorResponseFromException(
                    ex, userMessage);
            }
        }

        // GET properties
        /// <summary>
        /// Gets an photo for a particular main photo id
        /// </summary>
        /// <param name="mainPhotoId">A string that identifies an photo</param>
        /// <returns>A photo matching the specified main photo id</returns>
        /// <response code="200">Returns a photo</response>
        /// <response code="404">If the main photo id does not return any result</response>
        /// <response code="400">If the photo id is not valid</response>   
        /// <response code="500">If any errors are encountered</response> 
        [HttpGet("mainphoto/{mainPhotoId}")]
        public async Task<IActionResult> GetMainPhoto(string mainPhotoId)
        {
            if (!IdValidator.ValidateId(mainPhotoId))
            {
                var developerMessage = $"Invalid parameter - mainPhotoId";
                var userMessage = "Please provide a valid main photo id";

                return ResponseBuilder.BuildErrorResponse(
                    userMessage, developerMessage, (int)HttpStatusCode.BadRequest);
            }

            try
            {
                var asbestosActions = new AsbestosActions(_asbestosService, _loggerActions);
                var response = await asbestosActions.GetMainPhoto(mainPhotoId);
                return File(response.Data, response.ContentType);
            }
            catch (MissingFileException ex)
            {
                var developerMessage = ex.Message;
                var userMessage = "Cannot find file";

                return ResponseBuilder.BuildErrorResponse(
                    userMessage, developerMessage, (int)HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                var userMessage = "We had some problems processing your request";
                return ResponseBuilder.BuildErrorResponseFromException(
                    ex, userMessage);
            }
        }

        // GET properties
        /// <summary>
        /// Gets a list of documents related to a main photo for a propertyId
        /// </summary>
        /// <param name="propertyId">A string that identifies an property</param>
        /// <returns>A list of documents related to a main photo</returns>
        /// <response code="200">Returns a list of documents</response>
        /// <response code="404">If the property id does not return any result</response>
        /// <response code="400">If the property id is not valid</response>   
        /// <response code="500">If any errors are encountered</response> 
        [HttpGet("mainphoto")]
        public async Task<JsonResult> GetMainPhotoDocuments(string propertyId)
        {
            if (propertyId == null)
            {
                var developerMessage = $"Missing parameter - propertyId";
                var userMessage = "Please provide a valid property id";

                return ResponseBuilder.BuildErrorResponse(
                    userMessage, developerMessage, (int)HttpStatusCode.BadRequest);
            }
            if (!IdValidator.ValidatePropertyId(propertyId))
            {
                var developerMessage = $"Invalid parameter - propertyId";
                var userMessage = "Please provide a valid property id";

                return ResponseBuilder.BuildErrorResponse(
                    userMessage, developerMessage, (int)HttpStatusCode.BadRequest);
            }

            try
            {
                var asbestosActions = new AsbestosActions(_asbestosService, _loggerActions);
                var response = await asbestosActions.GetMainPhotoDocuments(propertyId);
                return ResponseBuilder.BuildSuccessResponse(response);
            }
            catch (MissingDocumentException ex)
            {
                var developerMessage = ex.Message;
                var userMessage = "Cannot find document";

                return ResponseBuilder.BuildErrorResponse(
                    userMessage, developerMessage, (int)HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                var userMessage = "We had some problems processing your request";
                return ResponseBuilder.BuildErrorResponseFromException(
                    ex, userMessage);
            }
        }

        // GET properties
        /// <summary>
        /// Gets an report for a particular report id
        /// </summary>
        /// <param name="reportId">A string that identifies an report id</param>
        /// <returns>A report in pdf format matching the specified report id</returns>
        /// <response code="200">Returns a report</response>
        /// <response code="404">If the report id does not return any result</response>
        /// <response code="400">If the report id is not valid</response>   
        /// <response code="500">If any errors are encountered</response> 
        [HttpGet("report/{reportId}")]
        public async Task<IActionResult> GetReport(string reportId)
        {
            if (!IdValidator.ValidateId(reportId))
            {
                var developerMessage = $"Invalid parameter - reportId";
                var userMessage = "Please provide a valid report id";

                return ResponseBuilder.BuildErrorResponse(
                    userMessage, developerMessage, (int)HttpStatusCode.BadRequest);
            }

            try
            {
                var asbestosActions = new AsbestosActions(_asbestosService, _loggerActions);
                var response = await asbestosActions.GetReport(reportId);
                return File(response.Data, response.ContentType);
            }
            catch (MissingFileException ex)
            {
                var developerMessage = ex.Message;
                var userMessage = "Cannot find file";

                return ResponseBuilder.BuildErrorResponse(
                    userMessage, developerMessage, (int)HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                var userMessage = "We had some problems processing your request";
                return ResponseBuilder.BuildErrorResponseFromException(
                    ex, userMessage);
            }
        }

        // GET properties
        /// <summary>
        /// Gets a list of documents related to a report for a propertyId
        /// </summary>
        /// <param name="propertyId">A string that identifies an property</param>
        /// <returns>A list of documents related to a report</returns>
        /// <response code="200">Returns a list of documents</response>
        /// <response code="404">If the property id does not return any result</response>
        /// <response code="400">If the property id is not valid</response>   
        /// <response code="500">If any errors are encountered</response> 
        [HttpGet("report")]
        public async Task<JsonResult> GetReportDocuments(string propertyId)
        {
            if (propertyId == null)
            {
                var developerMessage = $"Missing parameter - propertyId";
                var userMessage = "Please provide a valid property id";

                return ResponseBuilder.BuildErrorResponse(
                    userMessage, developerMessage, (int)HttpStatusCode.BadRequest);
            }
            if (!IdValidator.ValidatePropertyId(propertyId))
            {
                var developerMessage = $"Invalid parameter - propertyId";
                var userMessage = "Please provide a valid property id";

                return ResponseBuilder.BuildErrorResponse(
                    userMessage, developerMessage, (int)HttpStatusCode.BadRequest);
            }

            try
            {
                var asbestosActions = new AsbestosActions(_asbestosService, _loggerActions);
                var response = await asbestosActions.GetReportDocuments(propertyId);
                return ResponseBuilder.BuildSuccessResponse(response);
            }
            catch (MissingDocumentException ex)
            {
                var developerMessage = ex.Message;
                var userMessage = "Cannot find document";

                return ResponseBuilder.BuildErrorResponse(
                    userMessage, developerMessage, (int)HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                var userMessage = "We had some problems processing your request";
                return ResponseBuilder.BuildErrorResponseFromException(
                    ex, userMessage);
            }
        }

        // GET properties
        /// <summary>
        /// Gets an report for a particular main drawing id
        /// </summary>
        /// <param name="mainDrawingId">A string that identifies a main drawing id</param>
        /// <returns>An image matching the specified main drawing id</returns>
        /// <response code="200">Returns an image</response>
        /// <response code="404">If the main drawing id does not return any result</response>
        /// <response code="400">If the main drawing id is not valid</response>   
        /// <response code="500">If any errors are encountered</response> 
        [HttpGet("drawing/{mainDrawingId}")]
        public async Task<IActionResult> GetDrawing(string mainDrawingId)
        {
            if (!IdValidator.ValidateId(mainDrawingId))
            {
                var developerMessage = $"Invalid parameter - mainDrawingId";
                var userMessage = "Please provide a valid main drawing id";

                return ResponseBuilder.BuildErrorResponse(
                    userMessage, developerMessage, (int)HttpStatusCode.BadRequest);
            }

            try
            {
                var asbestosActions = new AsbestosActions(_asbestosService, _loggerActions);
                var response = await asbestosActions.GetDrawing(mainDrawingId);
                return File(response.Data, response.ContentType);
            }
            catch (MissingFileException ex)
            {
                var developerMessage = ex.Message;
                var userMessage = "Cannot find file";

                return ResponseBuilder.BuildErrorResponse(
                    userMessage, developerMessage, (int)HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                var userMessage = "We had some problems processing your request";
                return ResponseBuilder.BuildErrorResponseFromException(
                    ex, userMessage);
            }
        }

        // GET properties
        /// <summary>
        /// Gets a list of documents related to a drawing for a propertyId
        /// </summary>
        /// <param name="propertyId">A string that identifies an property</param>
        /// <returns>A list of documents related to a drawing</returns>
        /// <response code="200">Returns a list of documents</response>
        /// <response code="404">If the property id does not return any result</response>
        /// <response code="400">If the property id is not valid</response>   
        /// <response code="500">If any errors are encountered</response> 
        [HttpGet("drawing")]
        public async Task<JsonResult> GetDrawingDocuments(string propertyId)
        {
            if (propertyId == null)
            {
                var developerMessage = $"Missing parameter - propertyId";
                var userMessage = "Please provide a valid property id";

                return ResponseBuilder.BuildErrorResponse(
                    userMessage, developerMessage, (int)HttpStatusCode.BadRequest);
            }
            if (!IdValidator.ValidatePropertyId(propertyId))
            {
                var developerMessage = $"Invalid parameter - propertyId";
                var userMessage = "Please provide a valid property id";

                return ResponseBuilder.BuildErrorResponse(
                    userMessage, developerMessage, (int)HttpStatusCode.BadRequest);
            }

            try
            {
                var asbestosActions = new AsbestosActions(_asbestosService, _loggerActions);
                var response = await asbestosActions.GetDrawingDocuments(propertyId);
                return ResponseBuilder.BuildSuccessResponse(response);
            }
            catch (MissingDocumentException ex)
            {
                var developerMessage = ex.Message;
                var userMessage = "Cannot find document";

                return ResponseBuilder.BuildErrorResponse(
                    userMessage, developerMessage, (int)HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                var userMessage = "We had some problems processing your request";
                return ResponseBuilder.BuildErrorResponseFromException(
                    ex, userMessage);
            }
        }
    }
}