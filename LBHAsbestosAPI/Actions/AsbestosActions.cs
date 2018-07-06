using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LBHAsbestosAPI.Entities;
using LBHAsbestosAPI.Interfaces;

namespace LBHAsbestosAPI.Actions
{
	public class AsbestosActions : IAsbestosActions
    {
        IAsbestosService _asbestosService;
        ILoggerAdapter<AsbestosActions> _logger;

        public AsbestosActions(IAsbestosService asbestosService, ILoggerAdapter<AsbestosActions> logger)
        {
            _asbestosService = asbestosService;
            _logger = logger;
        }

		public async Task<IEnumerable<Inspection>> GetInspection(string propertyId)
		{
            _logger.LogInformation("yeah actions");
			IEnumerable<Inspection> lInspection = await _asbestosService.GetInspection(propertyId);

            if (lInspection.Any() == false)
            {
                throw new MissingInspectionException();
            }

			return lInspection;
		}
	}

    public class MissingInspectionException : Exception { }
}
