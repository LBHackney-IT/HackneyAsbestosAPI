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
        [HttpGet("photo/{photoId}")]
        public async Task<IActionResult> GetPhoto(string photoId)
        {
            return await documentResponseHelper(photoId, FileType.photo);
        }

        [HttpGet("photo")]
        public async Task<IActionResult> GetPhotoByInspectionId(string inspectionId)
        {
            throw new NotImplementedException();
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
        public async Task<IActionResult> getMainPhoto(string mainPhotoId)
        {
            return await documentResponseHelper(mainPhotoId, FileType.mainPhoto);
        }

        [HttpGet("mainphoto")]
        public async Task<IActionResult> GetMainPhotoByInspectionId(string inspectionId)
        {
            throw new NotImplementedException();
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
        public async Task<IActionResult> getReport(string reportId)
        {
            return await documentResponseHelper(reportId, FileType.report);
        }

        [HttpGet("report")]
        public async Task<IActionResult> GetReportByInspectionId(string inspectionId)
        {
            throw new NotImplementedException();
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
        public async Task<IActionResult> getDrawing(string drawingId)
        {
            return await documentResponseHelper(drawingId, FileType.drawing);
        }

        [HttpGet("drawing")]
        public async Task<IActionResult> GetDrawingByInspectionId(string inspectionId)
        {
            throw new NotImplementedException();
        }

        private async Task<IActionResult> documentResponseHelper(string fileId, string fileType)
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
    }

    public static class FileType
    {
        public const string photo = "photo";
        public const string report = "reports";
        public const string drawing = "maindrawing";
        public const string mainPhoto = "mainphoto";
    }
}
