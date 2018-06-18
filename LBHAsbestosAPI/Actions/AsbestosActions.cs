using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LBHAsbestosAPI.Entities;
using LBHAsbestosAPI.Interfaces;

namespace LBHAsbestosAPI.Actions
{
	public class AsbestosActions : IAsbestosActions
    {
		IAsbestosService _asbestosService;
        

		public AsbestosActions(IAsbestosService asbestosService)
        {
			_asbestosService = asbestosService;
            
        }

		public async Task<IEnumerable<Inspection>> GetInspection(string propertyId)
		{
			IEnumerable<Inspection> lInspection = await _asbestosService.GetInspection(propertyId);
			return lInspection;
		}
	}
}
