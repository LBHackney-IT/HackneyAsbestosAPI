using System.Collections.Generic;
using System.Threading.Tasks;
using LBHAsbestosAPI.Entities;

namespace LBHAsbestosAPI.Actions
{
    public interface IAsbestosActions1
    {
        Task<Floor> GetFloor(string floorId);
        Task<IEnumerable<Inspection>> GetInspection(string propertyId);
        Task<Room> GetRoom(string roomId);
    }
}