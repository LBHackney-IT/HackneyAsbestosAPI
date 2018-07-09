using System.Collections.Generic;
using System.Threading.Tasks;
using LBHAsbestosAPI.Entities;

namespace LBHAsbestosAPI.Interfaces
{
    public interface IPsi2000Api
    {
        IEnumerable<Element> GetElement(int elementId);
        IEnumerable<Floor> GetFloor(int floorId);
        Task<InspectionResponse> GetInspections(string propertyId);
        Task<RoomResponse> GetRoom(string roomId);
        Task<bool> Login();
    }
}