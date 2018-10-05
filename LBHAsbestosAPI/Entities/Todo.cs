using System;
namespace LBHAsbestosAPI.Entities
{
    public class Todo
    {
        public int Id { get; set; }
        public int JobId { get; set; }
        public string Description { get; set; }
        public int? FloorId { get; set; }
        public int? RoomId { get; set; }
        public int? ElementId { get; set; }
        public string LocationDescription { get; set; }
        public int PropertyId { get; set; }
        public string TodoReference { get; set; }
        public DateTime? DateOfCompletion { get; set; }
        public bool IsCompleted { get; set; }
        public string Field1 { get; set; }
        public string InspectedBy { get; set; }
        public string Uprn { get; set; }
        public int? PhotoId { get; set; }
        public int? DrawingId { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? DateOfModification { get; set; }
        public DateTime DateOfCreation { get; set; }
        public DateTime? DateOfRecorded { get; set; }
        public string Field2 { get; set; }
        public bool IsApproved { get; set; }
    }
}
