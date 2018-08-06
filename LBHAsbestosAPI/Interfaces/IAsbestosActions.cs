using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LBHAsbestosAPI.Controllers;
using LBHAsbestosAPI.Entities;
using Microsoft.Extensions.Logging;

namespace LBHAsbestosAPI.Actions
{
    public interface IAsbestosActions
    {
        Task<IEnumerable<Inspection>> GetInspection(string propertyId);
        Task<Floor> GetFloor(string floorId);
        Task<Room> GetRoom(string roomId);
        Task<Element> GetElement(string elementId);
    }
}
