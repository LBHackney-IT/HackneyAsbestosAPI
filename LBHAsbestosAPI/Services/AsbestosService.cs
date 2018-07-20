using System;
using System.Collections.Generic;
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

            if (TestStatus.IsRunningTests)
            {
                _api = AsbestosRepositoryFactory.Build();
            }
            else
            {
                _api = api;
            }
        }

        public Task<IEnumerable<Element>> GetElements(int elementId)
        {
            throw new NotImplementedException();
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
    }
}
