using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LBHAsbestosAPI.Entities;

namespace LBHAsbestosAPI.Interfaces
{
    public interface IAsbestosService
    {
		Task<IEnumerable<Inspection>> GetInspection(string propertyId);
		Task<IEnumerable<Floor>> GetFloor(int floorId);
		Task<Room> GetRoom(string roomId);
		Task<IEnumerable<Element>> GetElements(int elementId);
    }
}
