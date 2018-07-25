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
            var _asbestosActions = new AsbestosActions(_asbestosService, _loggerActions);

            try
            {
                var response = await _asbestosService.GetFile(photoId);
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
}
