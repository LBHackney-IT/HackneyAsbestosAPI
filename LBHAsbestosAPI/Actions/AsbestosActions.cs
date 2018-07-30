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
			var lInspection = await _asbestosService.GetInspection(propertyId);

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
            var room = await _asbestosService.GetRoom(roomId);

            if (room == null)
            {
                _logger.LogError($"No room returned for {roomId}");
                throw new MissingRoomException();
            }
            return room;
        }

        public async Task<Floor> GetFloor(string floorId)
        {
            _logger.LogInformation($"Calling Getfloor() with {floorId}");
            var floor = await _asbestosService.GetFloor(floorId);

            if (floor == null)
            {
                _logger.LogError($"No floor returned for {floorId}");
                throw new MissingFloorException();
            }
            return floor;
        }

        public async Task<Element> GetElement(string elementId)
        {
            _logger.LogInformation($"Calling GetElement() with {elementId}");
            var element = await _asbestosService.GetElement(elementId);

            if (element == null)
            {
                _logger.LogError($"No element returned for {elementId}");
                throw new MissingElementException();
            }
            return element;
        }

        public async Task<FileResponse> GetFile(string fileId, string fileType)
        {

            var file = await _asbestosService.GetFile(fileId, fileType);

            if (file.Data == null)
            {
                throw new MissingFileException();
            }
            return file;
        }
	}

    public class MissingInspectionException : Exception { }
    public class MissingRoomException : Exception { }
    public class MissingFloorException : Exception { }
    public class MissingElementException : Exception { }
    public class MissingFileException : Exception { }
}
