using System.Collections.Generic;
using System.Threading.Tasks;
using LBHAsbestosAPI.Entities;
using LBHAsbestosAPI.Models;

namespace LBHAsbestosAPI.Interfaces
{
    public interface IPsi2000Api
    {
        Task<Response<IEnumerable<Inspection>>> GetInspections(string propertyId);
        Task<Response<Room>> GetRoom(string roomId);
        Task<Response<Floor>> GetFloor(string floorId);
        Task<Response<Element>> GetElement(string elementId);
        Task<Response<IEnumerable<Todo>>> GetTodosByPropertyId(string propertyId);
        Task<Response<Todo>> GetTodo(string todoId);
        Task<Response<IEnumerable<Document>>> GetDocuments(string propertyId, string fileType);
        Task<FileContainer> GetFile(string fileId, string fileType);
    }
}