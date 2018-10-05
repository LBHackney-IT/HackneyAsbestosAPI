using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LBHAsbestosAPI.Entities;
using LBHAsbestosAPI.Interfaces;
using LBHAsbestosAPI.Models;

namespace LBHAsbestosAPI.Actions
{
    public class AsbestosActions
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

        public async Task<FileContainer> GetPhoto(string photoId)
        {
            var photo = await _asbestosService.GetPhoto(photoId);
            if (photo.Data == null)
            {
                throw new MissingFileException();
            }
            return photo;
        }

        public async Task<IEnumerable<Document>> GetPhotoDocuments(string propertyId)
        {
            var lDocument = await _asbestosService.GetPhotoDocuments(propertyId);
            if (lDocument.Any() == false)
            {
                _logger.LogError($"No Documents returned for {propertyId}");
                throw new MissingDocumentException();
            }
            return lDocument;
        }

        public async Task<FileContainer> GetMainPhoto(string mainPhotoId)
        {
            var file = await _asbestosService.GetMainPhoto(mainPhotoId);
            if (file.Data == null)
            {
                throw new MissingFileException();
            }
            return file;
        }

        public async Task<IEnumerable<Document>> GetMainPhotoDocuments(string propertyId)
        {
            var lDocument = await _asbestosService.GetMainPhotoDocuments(propertyId);
            if (lDocument.Any() == false)
            {
                _logger.LogError($"No Documents returned for {propertyId}");
                throw new MissingDocumentException();
            }
            return lDocument;
        }

        public async Task<FileContainer> GetReport(string reportId)
        {
            var file = await _asbestosService.GetReport(reportId);
            if (file.Data == null)
            {
                throw new MissingFileException();
            }
            return file;
        }

        public async Task<IEnumerable<Document>> GetReportDocuments(string propertyId)
        {
            var lDocument = await _asbestosService.GetReportDocuments(propertyId);
            if (lDocument.Any() == false)
            {
                _logger.LogError($"No Documents returned for {propertyId}");
                throw new MissingDocumentException();
            }
            return lDocument;
        }

        public async Task<FileContainer> GetDrawing(string mainDrawingId)
        {
            var file = await _asbestosService.GetDrawing(mainDrawingId);
            if (file.Data == null)
            {
                throw new MissingFileException();
            }
            return file;
        }

        public async Task<IEnumerable<Document>> GetDrawingDocuments(string propertyId)
        {
            var lDocument = await _asbestosService.GetDrawingDocuments(propertyId);
            if (lDocument.Any() == false)
            {
                _logger.LogError($"No Documents returned for {propertyId}");
                throw new MissingDocumentException();
            }
            return lDocument;
        }
	}

    public class MissingInspectionException : Exception { }
    public class MissingRoomException : Exception { }
    public class MissingFloorException : Exception { }
    public class MissingElementException : Exception { }
    public class MissingDocumentException : Exception { }
    public class MissingFileException : Exception { }
}
