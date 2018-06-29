using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LBHAsbestosAPI.Actions;
using LBHAsbestosAPI.Entities;
using LBHAsbestosAPI.Interfaces;
using LBHAsbestosAPI.Models;
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
		
        [HttpGet("inspection/{propertyId}")]
        public async Task<JsonResult> GetInspection(string propertyId)    
		{
            var _asbestosActions = new AsbestosActions(_asbestosService);
            var response = await _asbestosActions.GetInspection(propertyId);

            // TODO example for returning an unsuccessful response
            //var resultsOutput = new Dictionary<string, ApiErrorMessage>()
            //{
            //    { "errors", new ApiErrorMessage()
            //        {
            //            userMessage = "XXX",
            //            developerMessage = "XXX"
            //        }}
            //};

            var resultsOutput = new Dictionary<string, IEnumerable<Inspection>>()
            {
                {"results", response}
            };

            return new JsonResult(resultsOutput)
            {
                StatusCode = 200
            };
		}
	}
}
