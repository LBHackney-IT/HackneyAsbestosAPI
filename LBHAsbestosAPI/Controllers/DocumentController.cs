using System;
using System.Threading.Tasks;
using LBHAsbestosAPI.Actions;
using LBHAsbestosAPI.Builders;
using LBHAsbestosAPI.Interfaces;
using LBHAsbestosAPI.Repositories;
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
            return await fileResponseHelper(photoId, FileType.photo);
        }

        [HttpGet("mainphoto/{mainPhotoId}")]
        public async Task<IActionResult> getMainPhoto(string mainPhotoId)
        {
            return await fileResponseHelper(mainPhotoId, FileType.mainPhoto);
        }

        [HttpGet("report/{reportId}")]
        public async Task<IActionResult> getReport(string reportId)
        {
            return await fileResponseHelper(reportId, FileType.report);
        }

        [HttpGet("drawing/{drawingId}")]
        public async Task<IActionResult> getDrawing(string drawingId)
        {
            return await fileResponseHelper(drawingId, FileType.drawing);
        }

        private async Task<IActionResult> fileResponseHelper(string fileId, string fileType)
        {
            var _asbestosActions = new AsbestosActions(_asbestosService, _loggerActions);

            try
            {
                var response = await _asbestosService.GetFile(fileId, fileType);
                return File(response.DataStream, response.ContentType);
            }
            catch (MissingFileException ex)
            {
                var developerMessage = ex.Message;
                var userMessage = "Cannot find file";

                var responseBuilder = new ErrorResponseBuilder();
                return responseBuilder.BuildErrorResponse(
                userMessage, developerMessage, 404);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }

    public static class FileType
    {
        public static string photo = "photo";
        public static string report = "reports";
        public static string drawing = "maindrawing";
        public static string mainPhoto = "mainphoto";
    }
}
