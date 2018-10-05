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
    [Route("api/v1/documents")]
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

        // GET photo
        /// <summary>
        /// Returns a photo
        /// </summary>
        /// <param name="photoId">PSI2000 photo id</param>
        /// <returns>A photo matching the specified photo id</returns>
        /// <response code="200">Returns a photo</response>
        /// <response code="400">Id is not valid</response>   
        /// <response code="404">Photo not found for given id</response>
        /// <response code="500">Internal server error</response> 
        [HttpGet("photos/{photoId}")]
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

        // GET photo documents
        /// <summary>
        /// Gets a list of documents about photos
        /// </summary>
        /// <param name="propertyId">Universal Housing property id</param>
        /// <returns>A list of documents about photos matching the property id</returns>
        /// <response code="200">Returns a list of documents</response>
        /// <response code="404">No documents found for given id</response>
        /// <response code="400">id is not valid</response>   
        /// <response code="500">Internal server error</response> 
        [HttpGet("photos")]
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

        // GET main photo
        /// <summary>
        /// Returns a photo
        /// </summary>
        /// <param name="mainPhotoId">PSI2000 main photo id</param>
        /// <returns>A photo matching the specified main photo id</returns>
        /// <response code="200">Returns a photo</response>
        /// <response code="404">Photo not found for given id</response>
        /// <response code="400">Id is not valid</response>   
        /// <response code="500">Internal server error</response> 
        [HttpGet("mainphotos/{mainPhotoId}")]
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

        // GET main photo documents
        /// <summary>
        /// Gets a list of documents about main photos
        /// </summary>
        /// <param name="propertyId">Universal Housing property id</param>
        /// <returns>A list of documents about main photos matching the property id</returns>
        /// <response code="200">Returns a list of documents</response>
        /// <response code="404">No documents found for given id</response>
        /// <response code="400">Id is not valid</response>   
        /// <response code="500">Internal server error</response> 
        [HttpGet("mainphotos")]
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

        // GET report
        /// <summary>
        /// Returns a report in pdf format
        /// </summary>
        /// <param name="reportId">PSI2000 report id</param>
        /// <returns>A report matching the specified report id</returns>
        /// <response code="200">Returns a report</response>
        /// <response code="404">Report not found for given id</response>
        /// <response code="400">Id is not valid</response>   
        /// <response code="500">Internal server error</response> 
        [HttpGet("reports/{reportId}")]
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

        // GET report documents
        /// <summary>
        /// Gets a list of documents about reports
        /// </summary>
        /// <param name="propertyId">Universal Housing property Id</param>
        /// <returns>A list of documents about reports matching the property Id</returns>
        /// <response code="200">Returns a list of documents</response>
        /// <response code="404">No documents found for given id</response>
        /// <response code="400">Id is not valid</response>   
        /// <response code="500">Internal server error</response> 
        [HttpGet("reports")]
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

        // GET drawing
        /// <summary>
        /// Returns a drawing
        /// </summary>
        /// <param name="mainDrawingId">PSI2000 main drawing id</param>
        /// <returns>A drawing matching the specified drawing id</returns>
        /// <response code="200">Returns a drawing</response>
        /// <response code="404">Drawing not found for given id</response>
        /// <response code="400">Id is not valid</response>   
        /// <response code="500">Internal server error</response> 
        [HttpGet("drawings/{mainDrawingId}")]
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

        // GET drawing documents
        /// <summary>
        /// Gets a list of documents about drawings
        /// </summary>
        /// <param name="propertyId">Universal Housing property Id</param>
        /// <returns>A list of documents about drawings matching the property Id</returns>
        /// <response code="200">Returns a list of documents</response>
        /// <response code="404">No documents found for given id</response>
        /// <response code="400">Id is not valid</response>   
        /// <response code="500">Internal server error</response> 
        [HttpGet("drawings")]
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