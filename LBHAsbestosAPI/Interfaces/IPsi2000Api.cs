using System.Collections.Generic;
using System.Threading.Tasks;
using LBHAsbestosAPI.Entities;

namespace LBHAsbestosAPI.Interfaces
{
    public interface IPsi2000Api
    {
        Task<Response<IEnumerable<Inspection>>> GetInspections(string propertyId);
        Task<RoomResponse> GetRoom(string roomId);
        Task<FloorResponse> GetFloor(string floorId);
        Task<ElementResponse> GetElement(string elementId);
        Task<DocumentResponse> GetDocument(string propertyId, string fileType);
        Task<FileContainer> GetFile(string fileId, string fileType);
    }
}