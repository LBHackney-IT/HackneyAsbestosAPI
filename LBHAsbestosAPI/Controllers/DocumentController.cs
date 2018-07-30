using System;
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

        [HttpGet("photo/{photoId}")]
        public async Task<IActionResult> getPhoto(string photoId)
        {
            //throw new NotImplementedException();
            return await documentResponseHelper(photoId, FileType.photo);
        }

        [HttpGet("mainphoto/{mainPhotoId}")]
        public async Task<IActionResult> getMainPhoto(string mainPhotoId)
        {
            //throw new NotImplementedException();
            return await documentResponseHelper(mainPhotoId, FileType.mainPhoto);
        }

        [HttpGet("report/{reportId}")]
        public async Task<IActionResult> getReport(string reportId)
        {
            //throw new NotImplementedException();
            return await documentResponseHelper(reportId, FileType.report);
        }

        [HttpGet("drawing/{drawingId}")]
        public async Task<IActionResult> getDrawing(string drawingId)
        {
            //throw new NotImplementedException();
            return await documentResponseHelper(drawingId, FileType.drawing);
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
                        userMessage, developerMessage, 400);
                }
                var response = await _asbestosActions.GetFile(fileId, fileType);
                return File(response.Data, response.ContentType);
            }
            catch (MissingFileException ex)
            {
                var developerMessage = ex.Message;
                var userMessage = "Cannot find file";

                return new ErrorResponseBuilder().BuildErrorResponse(
                userMessage, developerMessage, 404);
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
