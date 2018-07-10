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
            _logger.LogInformation($"Calling GetInspection() with {propertyId}");
			IEnumerable<Inspection> lInspection = await _asbestosService.GetInspection(propertyId);

            if (lInspection.Any() == false)
            {
                _logger.LogError($"No inspections returned for {propertyId}");
                throw new MissingInspectionException();
            }
			return lInspection;
		}

        public async Task<Room> GetRoom(string roomId)
        {
            _logger.LogInformation($"Calling GetRoom() with {roomId}");
            Room room = await _asbestosService.GetRoom(roomId);

            if (room.Id == 0)
            {
                _logger.LogError($"No rooms returned for {roomId}");
                throw new MissingRoomException();
            }
            return room;
        }
	}

    public class MissingInspectionException : Exception { }
    public class MissingRoomException : Exception { }
}
