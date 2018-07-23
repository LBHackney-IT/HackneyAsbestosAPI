using System.Collections.Generic;
using System.Threading.Tasks;
using LBHAsbestosAPI.Entities;

namespace LBHAsbestosAPI.Interfaces
{
    public interface IPsi2000Api
    {
        Task<InspectionResponse> GetInspections(string propertyId);
        Task<RoomResponse> GetRoom(string roomId);
        Task<FloorResponse> GetFloor(string floorId);
        Task<ElementResponse> GetElement(string elementId);
        Task<bool> Login();
    }
}