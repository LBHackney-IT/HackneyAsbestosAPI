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
        /// Returns a photo
        /// </summary>
        /// <param name="photoId">PSI2000 photo id</param>
        /// <returns>A photo matching the specified photo id</returns>
        /// <response code="200">Returns a photo</response>
        /// <response code="404">Photo not found for given id</response>
        /// <response code="400">Id is not valid</response>   
        /// <response code="500">Internal server error</response> 
        [HttpGet("photo/{photoId}")]
        public async Task<IActionResult> GetPhoto(string photoId)
        {
            return await FileResponseHelper(photoId, FileType.photo);
        }

        [HttpGet("photo")]
        public async Task<JsonResult> GetPhotoByPropertyId(string propertyId)
        {
            return await DocumentResponseHelper(propertyId, FileType.photo);
        }

        // GET properties
        /// <summary>
        /// Returns a photo
        /// </summary>
        /// <param name="mainPhotoId">PSI2000 main photo id</param>
        /// <returns>A photo matching the specified main photo id</returns>
        /// <response code="200">Returns a photo</response>
        /// <response code="404">Photo not found for given id</response>
        /// <response code="400">Id is not valid</response>   
        /// <response code="500">Internal server error</response> 
        [HttpGet("mainphoto/{mainPhotoId}")]
        public async Task<IActionResult> GetMainPhoto(string mainPhotoId)
        {
            return await FileResponseHelper(mainPhotoId, FileType.mainPhoto);
        }

        [HttpGet("mainphoto")]
        public async Task<JsonResult> GetMainPhotoByPropertyId(string propertyId)
        {
            return await DocumentResponseHelper(propertyId, FileType.mainPhoto);
        }

        // GET properties
        /// <summary>
        /// Returns a report in pdf format
        /// </summary>
        /// <param name="reportId">PSI2000 report id</param>
        /// <returns>A report matching the specified report id</returns>
        /// <response code="200">Returns a report</response>
        /// <response code="404">Report not found for given id</response>
        /// <response code="400">Id is not valid</response>   
        /// <response code="500">Internal server error</response> 
        [HttpGet("report/{reportId}")]
        public async Task<IActionResult> GetReport(string reportId)
        {
            return await FileResponseHelper(reportId, FileType.report);
        }

        [HttpGet("report")]
        public async Task<JsonResult> GetReportByPropertyId(string propertyId)
        {
            return await DocumentResponseHelper(propertyId, FileType.report);
        }

        // GET properties
        /// <summary>
        /// Returns a drawing
        /// </summary>
        /// <param name="drawingId">PSI2000 drawing id</param>
        /// <returns>A drawing matching the specified drawing id</returns>
        /// <response code="200">Returns a drawing</response>
        /// <response code="404">Drawing not found for given id</response>
        /// <response code="400">Id is not valid</response>   
        /// <response code="500">Internal server error</response> 
        [HttpGet("drawing/{drawingId}")]
        public async Task<IActionResult> GetDrawing(string drawingId)
        {
            return await FileResponseHelper(drawingId, FileType.drawing);
        }

        [HttpGet("drawing")]
        public async Task<JsonResult> GetDrawingByPropertyId(string propertyId)
        {
            return await DocumentResponseHelper(propertyId, FileType.drawing);
        }

        private async Task<IActionResult> FileResponseHelper(string fileId, string fileType)
        {
            var _asbestosActions = new AsbestosActions(_asbestosService, _loggerActions);
            try
            {
                if (!IdValidator.ValidateId(fileId))
                {
                    var developerMessage = $"Invalid parameter - fileId";
                    var userMessage = "Please provide a valid file id";

                    return new ErrorResponseBuilder().BuildErrorResponse(
                        userMessage, developerMessage, (int)HttpStatusCode.BadRequest);
                }
                var response = await _asbestosActions.GetFile(fileId, fileType);
                return File(response.Data, response.ContentType);
            }
            catch (MissingFileException ex)
            {
                var developerMessage = ex.Message;
                var userMessage = "Cannot find file";

                return new ErrorResponseBuilder().BuildErrorResponse(
                    userMessage, developerMessage, (int)HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                var userMessage = "We had some problems processing your request";
                return new ErrorResponseBuilder().BuildErrorResponseFromException(
                    ex, userMessage);
            }
        }

        private async Task<JsonResult> DocumentResponseHelper(string propertyId, string fileType)
        {
            var _asbestosActions = new AsbestosActions(_asbestosService, _loggerActions);
            try
            {
                if (!IdValidator.ValidatePropertyId(propertyId))
                {
                    var developerMessage = $"Invalid parameter - propertyId";
                    var userMessage = "Please provide a valid file property id";

                    return new ErrorResponseBuilder().BuildErrorResponse(
                        userMessage, developerMessage, (int)HttpStatusCode.BadRequest);
                }
                var response = await _asbestosActions.GetDocument(propertyId, fileType);
                return new DocumentResponseBuilder().BuildSuccessResponse(response);
            }
            catch (MissingDocumentException ex)
            {
                var developerMessage = ex.Message;
                var userMessage = "Cannot find document";

                return new ErrorResponseBuilder().BuildErrorResponse(
                    userMessage, developerMessage, (int)HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                var a = ex.Message;
                var userMessage = "We had some problems processing your request";
                return new ErrorResponseBuilder().BuildErrorResponseFromException(
                    ex, userMessage);
            }
        }
    }

    public static class FileType
    {
        public const string photo = "photo";
        public const string report = "reports";
        public const string drawing = "maindrawing";
        public const string mainPhoto = "mainphoto";
    }
}
