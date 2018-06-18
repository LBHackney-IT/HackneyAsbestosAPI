using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LBHAsbestosAPI.Entities;
using LBHAsbestosAPI.Interfaces;
using LBHAsbestosAPI.Repositories;

namespace LBHAsbestosAPI.Services
{
	public class AsbestosService : IAsbestosService
    {
		Psi2000Api api;
		
        public AsbestosService()
        {
			api = new Psi2000Api();
        }

		public Task<IEnumerable<Element>> GetElements(int elementId)
		{
			throw new NotImplementedException();
		}

		public async Task<IEnumerable<Floor>> GetFloor(int floorId)
		{
			return api.GetFloor(floorId);
		}

		public async Task<IEnumerable<Inspection>> GetInspection(string propertyId)
		{
			InspectionResponse response = await api.GetInspections(propertyId);
			List<Inspection> r = response.Data;
			return r;
		}

		public Task<IEnumerable<Room>> GetRoom(int roomId)
		{
			throw new NotImplementedException();
		}
	}
}
