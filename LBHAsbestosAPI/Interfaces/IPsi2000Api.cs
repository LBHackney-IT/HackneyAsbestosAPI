using System.Collections.Generic;
using System.Threading.Tasks;
using LBHAsbestosAPI.Entities;

namespace LBHAsbestosAPI.Repositories
{
    public interface IPsi2000Api
    {
        IEnumerable<Element> GetElement(int elementId);
        IEnumerable<Floor> GetFloor(int floorId);
        Task<InspectionResponse> GetInspections(string propertyId);
        IEnumerable<Room> GetRoom(int roomId);
        Task<bool> Login();
    }
}