using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using LBHAsbestosAPI.Entities;
using LBHAsbestosAPI.Factories;
using LBHAsbestosAPI.Interfaces;

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
            _logger.LogInformation($"Calling GetRoom with {roomId}");
            var response = await _api.GetRoom(roomId);
            return response.Data;
        }

        public async Task<Floor> GetFloor(string floorId)
        {
            _logger.LogInformation($"Calling GetFloor with {floorId}");
            var response = await _api.GetFloor(floorId);
            return response.Data;
        }

        public async Task<Element> GetElement(string elementId)
        {
            _logger.LogInformation($"Calling GetElement with {elementId}");
            var response = await _api.GetElement(elementId);
            return response.Data; 
        }

        public async Task<IEnumerable<Document>> GetDocument(string inspectionId, string fileType)
        {
            throw new NotImplementedException();
        }

        public async Task<FileContainer> GetFile(string fileId, string fileType)
        {
            return await _api.GetFile(fileId, fileType);
        }
    }
}
