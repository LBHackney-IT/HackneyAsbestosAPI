using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LBHAsbestosAPI.Entities;
using LBHAsbestosAPI.Models;

namespace LBHAsbestosAPI.Actions
{
    public interface IAsbestosActions
    {
        Task<IEnumerable<Inspection>> GetInspection(string propertyId);
        Task<Floor> GetFloor(string floorId);
        Task<Room> GetRoom(string roomId);
        Task<Element> GetElement(string elementId);
        Task<FileContainer> GetFile(string fileId, string fileType);
    }
}
