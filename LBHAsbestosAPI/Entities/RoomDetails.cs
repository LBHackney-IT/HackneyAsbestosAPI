using System;
namespace LBHAsbestosAPI.Entities
{
    public class RoomDetails
    {
		public int Id { get; set; }
        public int FloorId { get; set; }
        public string Description { get; set; }
        public int PropertyId { get; set; }
        public string OrderId { get; set; }
        public int Uprn { get; set; }
        public bool IsInspected { get; set; }
        public bool IsDoesContainAsbestos { get; set; }
        public bool IsDidContainAsbestos { get; set; }
        public bool IsAnyToDos { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime DateOfModification { get; set; }
        public DateTime DateOfCreation { get; set; }
        public bool IsActive { get; set; }
        public bool IsLiableToAsbestos { get; set; }
    }
}
