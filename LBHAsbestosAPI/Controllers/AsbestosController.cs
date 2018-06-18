using System;
using System.Threading.Tasks;
using LBHAsbestosAPI.Actions;
using LBHAsbestosAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LBHAsbestosAPI.Controllers
{
	[Route("api/v1/")]
	public class AsbestosController : Controller
    {
		IAsbestosService _asbestosService; 
		public AsbestosController(IAsbestosService asbestosService)
        {
			_asbestosService = asbestosService;
        }
		[HttpGet("inspection/{id}")]
		public async Task<IActionResult> GetInspection(string propertyId)    
		{
			var _asbestosActions = new AsbestosActions(_asbestosService);
			var response = await _asbestosActions.GetInspection(propertyId);
			return Ok(new JsonResult(response));
		}
	}
}
