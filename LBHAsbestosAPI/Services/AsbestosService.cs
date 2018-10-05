using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LBHAsbestosAPI.Entities;
using LBHAsbestosAPI.Interfaces;
using LBHAsbestosAPI.Models;
using LBHAsbestosAPI.Repositories;

namespace LBHAsbestosAPI.Services
{
    public class AsbestosService : IAsbestosService
    {
        IPsi2000Api _api;
        ILoggerAdapter<AsbestosService> _logger;

        public AsbestosService(IPsi2000Api api, ILoggerAdapter<AsbestosService> logger)
        {
            _logger = logger;
            _api = api;
        }

        public async Task<IEnumerable<Inspection>> GetInspection(string propertyId)
        {
            _logger.LogInformation($"Calling GetInspections() with {propertyId}");
            var response = await _api.GetInspections(propertyId);
            var responseInspections = response.Data;
            return responseInspections;
        }

        public async Task<Room> GetRoom(string roomId)
        {
            _logger.LogInformation($"Calling GetRoom() with {roomId}");
            var response = await _api.GetRoom(roomId);
            return response.Data;
        }

        public async Task<Floor> GetFloor(string floorId)
        {
            _logger.LogInformation($"Calling GetFloor() with {floorId}");
            var response = await _api.GetFloor(floorId);
            return response.Data;
        }

        public async Task<Element> GetElement(string elementId)
        {
            _logger.LogInformation($"Calling GetElement() with {elementId}");
            var response = await _api.GetElement(elementId);
            return response.Data; 
        }

        public async Task<FileContainer> GetPhoto(string photoID)
        {
            _logger.LogInformation($"Calling GetPhoto() with {photoID}");
            var response = await _api.GetFile(photoID, FileType.photo);
            return response;
        }

        public async Task<IEnumerable<Document>> GetPhotoDocuments(string propertyId)
        {
            _logger.LogInformation($"Calling GetPhotoDocuments() with {propertyId}");
            var response = await GetDocument(propertyId, FileType.photo);
            return response;
        }

        public async Task<FileContainer> GetMainPhoto(string mainPhotoID)
        {
            _logger.LogInformation($"Calling GetMainPhoto() with {mainPhotoID}");
            var response = await _api.GetFile(mainPhotoID, FileType.mainPhoto);
            return response;
        }

        public async Task<IEnumerable<Document>> GetMainPhotoDocuments(string propertyId)
        {
            _logger.LogInformation($"Calling GetMainPhotoDocuments() with {propertyId}");
            var response = await GetDocument(propertyId, FileType.mainPhoto);
            return response;
        }

        public async Task<FileContainer> GetReport(string reportId)
        {
            _logger.LogInformation($"Calling GetReport() with {reportId}");
            var response = await _api.GetFile(reportId, FileType.report);
            return response;
        }

        public async Task<IEnumerable<Document>> GetReportDocuments(string propertyId)
        {
            _logger.LogInformation($"Calling GetReportDocuments() with {propertyId}");
            var response = await GetDocument(propertyId, FileType.report);
            return response;
        }

        public async Task<FileContainer> GetDrawing(string mainDrawingId)
        {
            _logger.LogInformation($"Calling GetDrawing() with {mainDrawingId}");
            var response =  await _api.GetFile(mainDrawingId, FileType.drawing);
            return response;
        }

        public async Task<IEnumerable<Document>> GetDrawingDocuments(string propertyId)
        {
            _logger.LogInformation($"Calling GetDrawingDocuments() with {propertyId}");
            var response = await GetDocument(propertyId, FileType.drawing);
            return response;
        }

        private async Task<IEnumerable<Document>> GetDocument(string propertyId, string fileType)
        {
            _logger.LogInformation($"Calling GetDocument() with {propertyId}");
            var response = await _api.GetDocuments(propertyId, fileType);
            var responseInspections = response.Data;
            return responseInspections;
        }

        public async Task<IEnumerable<Todo>> GetTodosByPropertyId(string propertyId)
        {
            _logger.LogInformation($"Calling GetTodosByPropertyId() with {propertyId}");
            var response = await _api.GetTodosByPropertyId(propertyId);
            var responseTodos = response.Data;
            return responseTodos; 
        }

        public async Task<Todo> GetTodo(string todoId)
        {
            _logger.LogInformation($"Calling GetTodo() with {todoId}");
            var response = await _api.GetTodo(todoId);
            return response.Data;        
        }
    }
}
