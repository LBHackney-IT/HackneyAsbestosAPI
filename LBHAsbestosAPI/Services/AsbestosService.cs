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

        public AsbestosService(IPsi2000Api api)
        {
            // TODO temporary workaround
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

		public async Task<IEnumerable<Floor>> GetFloor(int floorId)
		{
			return _api.GetFloor(floorId);
		}

		public async Task<IEnumerable<Inspection>> GetInspection(string propertyId)
		{
			InspectionResponse response = await _api.GetInspections(propertyId);
            List<Inspection> r = response.Data;
			return r;
		}

		public Task<IEnumerable<Room>> GetRoom(int roomId)
		{
			throw new NotImplementedException();
		}
	}
}
