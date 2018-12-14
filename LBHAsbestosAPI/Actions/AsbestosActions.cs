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
            _logger.LogInformation($"Calling GetPhoto() with {photoId}");
            var photo = await _asbestosService.GetPhoto(photoId);
            if (photo.Data == null)
            {
                _logger.LogError($"No photos returned for {photoId}");
                throw new MissingFileException();
            }
            return photo;
        }

        public async Task<IEnumerable<Document>> GetPhotoDocuments(string propertyId)
        {
            _logger.LogInformation($"Calling GetPhotoDocuments() with {propertyId}");
            var lDocument = await _asbestosService.GetPhotoDocuments(propertyId);
            return lDocument;
        }

        public async Task<FileContainer> GetMainPhoto(string mainPhotoId)
        {
            _logger.LogInformation($"Calling GetMainPhoto() with {mainPhotoId}");
            var file = await _asbestosService.GetMainPhoto(mainPhotoId);
            if (file.Data == null)
            {
                _logger.LogError($"No main photos returned for {mainPhotoId}");
                throw new MissingFileException();
            }
            return file;
        }

        public async Task<IEnumerable<Document>> GetMainPhotoDocuments(string propertyId)
        {
            _logger.LogInformation($"Calling GetMainPhotoDocuments() with {propertyId}");
            var lDocument = await _asbestosService.GetMainPhotoDocuments(propertyId);
            return lDocument;
        }

        public async Task<FileContainer> GetReport(string reportId)
        {
            _logger.LogInformation($"Calling GetReport() with {reportId}");

            var file = await _asbestosService.GetReport(reportId);
            if (file.Data == null)
            {
                _logger.LogError($"No report returned for {reportId}");
                throw new MissingFileException();
            }
            return file;
        }

        public async Task<IEnumerable<Document>> GetReportDocuments(string propertyId)
        {
            _logger.LogInformation($"Calling GetReportDocuments() with {propertyId}");
            var lDocument = await _asbestosService.GetReportDocuments(propertyId);
            return lDocument;
        }

        public async Task<FileContainer> GetDrawing(string mainDrawingId)
        {
            _logger.LogInformation($"Calling GetDrawing() with {mainDrawingId}");
            var file = await _asbestosService.GetDrawing(mainDrawingId);
            if (file.Data == null)
            {
                _logger.LogError($"No Drawing returned for {mainDrawingId}");
                throw new MissingFileException();
            }
            return file;
        }

        public async Task<IEnumerable<Document>> GetDrawingDocuments(string propertyId)
        {
            _logger.LogInformation($"Calling GetDrawingDocuments() with {propertyId}");
            var lDocument = await _asbestosService.GetDrawingDocuments(propertyId);
            return lDocument;
        }

        public async Task<IEnumerable<Todo>> GetTodosByPropertyId(string propertyId)
        {
            _logger.LogInformation($"Calling GetTodosByPropertyId() with {propertyId}");
            var lTodo = await _asbestosService.GetTodosByPropertyId(propertyId);
            return lTodo;
        }

        public async Task<Todo> GetTodo(string todoId)
        {
            _logger.LogInformation($"Calling GetTodo() with {todoId}");
            var todo = await _asbestosService.GetTodo(todoId);

            if (todo == null)
            {
                _logger.LogError($"No todo returned for {todoId}");
                throw new MissingTodoException();
            }
            return todo;
        }

        public async Task<IEnumerable<Sample>> GetSamples(string inspectionId)
        {
            _logger.LogInformation($"Calling GetSamples() with {inspectionId}");
            var samples = await _asbestosService.GetSamples(inspectionId);
            return samples;
        }
	}

    public class MissingRoomException : Exception { }
    public class MissingFloorException : Exception { }
    public class MissingElementException : Exception { }
    public class MissingDocumentException : Exception { }
    public class MissingFileException : Exception { }
    public class MissingTodoException : Exception { }
}
