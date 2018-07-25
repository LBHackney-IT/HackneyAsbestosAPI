using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LBHAsbestosAPI.Entities;

namespace LBHAsbestosAPI.Interfaces
{
    public interface IAsbestosService
    {
		Task<IEnumerable<Inspection>> GetInspection(string propertyId);
		Task<Room> GetRoom(string roomId);
        Task<Floor> GetFloor(string floorId);
		Task<Element> GetElement(string elementId);
        Task<FileResponse> GetFile(string fileId);
    }
}
