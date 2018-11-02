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
                return ResponseBuilder.Error(400, "Please provide a valid photo id", "Invalid parameter - photoId");
            }

            try
            {
                var asbestosActions = new AsbestosActions(_asbestosService, _loggerActions);
                var response = await asbestosActions.GetPhoto(photoId);
                return File(response.Data, response.ContentType);
            }
            catch (MissingFileException ex)
            {
                return ResponseBuilder.Error(404, "Cannot find file", ex.Message);
            }
            catch (Exception ex)
            {
                return ResponseBuilder.Error(500, "We had some issues processing your request", ex.Message);
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
                return ResponseBuilder.Error(400, "Please provide a valid property id", "Missing parameter - propertyId");
            }
            if (!IdValidator.ValidatePropertyId(propertyId))
            {
                return ResponseBuilder.Error(400, "Please provide a valid property id", "Invalid parameter - propertyId");
            }

            try
            {
                var asbestosActions = new AsbestosActions(_asbestosService, _loggerActions);
                var response = await asbestosActions.GetPhotoDocuments(propertyId);
                return ResponseBuilder.Ok(response);
            }
            catch (MissingDocumentException ex)
            {
                return ResponseBuilder.Error(404, "", ex.Message);
            }
            catch (Exception ex)
            {
                return ResponseBuilder.Error(500, "We had some issues processing your request", ex.Message);
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
                return ResponseBuilder.Error(400, "Please provide a valid main photo id", "Invalid parameter - mainPhotoId");
            }

            try
            {
                var asbestosActions = new AsbestosActions(_asbestosService, _loggerActions);
                var response = await asbestosActions.GetMainPhoto(mainPhotoId);
                return File(response.Data, response.ContentType);
            }
            catch (MissingFileException ex)
            {
                return ResponseBuilder.Error(404, "Cannot find file", ex.Message);
            }
            catch (Exception ex)
            {
                return ResponseBuilder.Error(500, "We had some issues processing your request", ex.Message);
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
                return ResponseBuilder.Error(400, "Please provide a valid main property id", "Missing parameter - propertyId");
            }
            if (!IdValidator.ValidatePropertyId(propertyId))
            {
                var developerMessage = $"Invalid parameter - propertyId";
                var userMessage = "Please provide a valid property id";

                return ResponseBuilder.Error(400,
                    userMessage, developerMessage);
            }

            try
            {
                var asbestosActions = new AsbestosActions(_asbestosService, _loggerActions);
                var response = await asbestosActions.GetMainPhotoDocuments(propertyId);
                return ResponseBuilder.Ok(response);
            }
            catch (MissingDocumentException ex)
            {
                return ResponseBuilder.Error(404, "Cannot find documents", ex.Message);
            }
            catch (Exception ex)
            {
                return ResponseBuilder.Error(500, "We had some issues processing your request", ex.Message);
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
                return ResponseBuilder.Error(400, "Please provide a valid main report id", "Missing parameter - reportId");
            }

            try
            {
                var asbestosActions = new AsbestosActions(_asbestosService, _loggerActions);
                var response = await asbestosActions.GetReport(reportId);
                return File(response.Data, response.ContentType);
            }
            catch (MissingFileException ex)
            {
                return ResponseBuilder.Error(404, "Cannot find file", ex.Message);
            }
            catch (Exception ex)
            {
                return ResponseBuilder.Error(500, "We had some issues processing your request", ex.Message);
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
                return ResponseBuilder.Error(400, "Please provide a valid property id", "Missing parameter - propertyId");
            }
            if (!IdValidator.ValidatePropertyId(propertyId))
            {
                return ResponseBuilder.Error(400, "Please provide a valid property id", "Invalid parameter - propertyId");
            }

            try
            {
                var asbestosActions = new AsbestosActions(_asbestosService, _loggerActions);
                var response = await asbestosActions.GetReportDocuments(propertyId);
                return ResponseBuilder.Ok(response);
            }
            catch (MissingDocumentException ex)
            {
                var developerMessage = ex.Message;
                var userMessage = "Cannot find documents";
                return ResponseBuilder.Error(404,
                    userMessage, developerMessage);
            }
            catch (Exception ex)
            {
                return ResponseBuilder.Error(500, "We had some issues processing your request", ex.Message);
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
                return ResponseBuilder.Error(400, "Please provide a valid main drawing id", "Missing parameter - mainDrawingId");
            }

            try
            {
                var asbestosActions = new AsbestosActions(_asbestosService, _loggerActions);
                var response = await asbestosActions.GetDrawing(mainDrawingId);
                return File(response.Data, response.ContentType);
            }
            catch (MissingFileException ex)
            {
                return ResponseBuilder.Error(404, "Cannot find file", ex.Message);
            }
            catch (Exception ex)
            {
                return ResponseBuilder.Error(500, "We had some issues processing your request", ex.Message);
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
                return ResponseBuilder.Error(400, "Please provide a valid property id", "Missing parameter - propertyId");
            }
            if (!IdValidator.ValidatePropertyId(propertyId))
            {
                return ResponseBuilder.Error(400, "Please provide a valid property id", "Invalid parameter - propertyId");
            }

            try
            {
                var asbestosActions = new AsbestosActions(_asbestosService, _loggerActions);
                var response = await asbestosActions.GetDrawingDocuments(propertyId);
                return ResponseBuilder.Ok(response);
            }
            catch (MissingDocumentException ex)
            {
                var developerMessage = ex.Message;
                var userMessage = "Cannot find document";

                return ResponseBuilder.Error(404,
                    userMessage, developerMessage);
            }
            catch (Exception ex)
            {
                return ResponseBuilder.Error(500, "We had some issues processing your request", ex.Message);
            }
        }
    }
}