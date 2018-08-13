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
            return await FileResponseHelper(photoId, FileType.photo);
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
        public async Task<JsonResult> GetPhotoByPropertyId(string propertyId)
        {
            return await DocumentResponseHelper(propertyId, FileType.photo);
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
            return await FileResponseHelper(mainPhotoId, FileType.mainPhoto);
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
        public async Task<JsonResult> GetMainPhotoByPropertyId(string propertyId)
        {
            return await DocumentResponseHelper(propertyId, FileType.mainPhoto);
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
            return await FileResponseHelper(reportId, FileType.report);
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
        public async Task<JsonResult> GetReportByPropertyId(string propertyId)
        {
            return await DocumentResponseHelper(propertyId, FileType.report);
        }

        // GET properties
        /// <summary>
        /// Gets an report for a particular drawing id
        /// </summary>
        /// <param name="drawingId">A string that identifies a drawing id</param>
        /// <returns>An image matching the specified drawing id</returns>
        /// <response code="200">Returns an image</response>
        /// <response code="404">If the drawing id does not return any result</response>
        /// <response code="400">If the drawing id is not valid</response>   
        /// <response code="500">If any errors are encountered</response> 
        [HttpGet("drawing/{drawingId}")]
        public async Task<IActionResult> GetDrawing(string drawingId)
        {
            return await FileResponseHelper(drawingId, FileType.drawing);
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

                    return ResponseBuilder.BuildErrorResponse(
                        userMessage, developerMessage, (int)HttpStatusCode.BadRequest);
                }
                var response = await _asbestosActions.GetFile(fileId, fileType);
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

        private async Task<JsonResult> DocumentResponseHelper(string propertyId, string fileType)
        {
            var _asbestosActions = new AsbestosActions(_asbestosService, _loggerActions);
            try
            {
                if (propertyId == null) 
                {
                    var developerMessage = $"Missing parameter - propertyId or file id";
                    var userMessage = "Please provide a valid property id or file id";

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
                var response = await _asbestosActions.GetDocument(propertyId, fileType);
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

    public static class FileType
    {
        public const string photo = "photo";
        public const string report = "reports";
        public const string drawing = "maindrawing";
        public const string mainPhoto = "mainphoto";
    }
}
