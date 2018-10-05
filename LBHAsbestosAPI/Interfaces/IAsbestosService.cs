using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LBHAsbestosAPI.Entities;
using LBHAsbestosAPI.Models;

namespace LBHAsbestosAPI.Interfaces
{
    public interface IAsbestosService
    {
        Task<IEnumerable<Inspection>> GetInspection(string propertyId);
        Task<Room> GetRoom(string roomId);
        Task<Floor> GetFloor(string floorId);
        Task<Element> GetElement(string elementId);
        Task<FileContainer> GetPhoto(string photoID);
        Task<IEnumerable<Document>> GetPhotoDocuments(string propertyId);
        Task<FileContainer> GetMainPhoto(string mainPhotoID);
        Task<IEnumerable<Document>> GetMainPhotoDocuments(string propertyId);
        Task<FileContainer> GetReport(string reportId);
        Task<IEnumerable<Document>> GetReportDocuments(string propertyId);
        Task<FileContainer> GetDrawing(string mainDrawingId);
        Task<IEnumerable<Document>> GetDrawingDocuments(string propertyId);
    }
}
