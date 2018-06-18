using System;
namespace LBHAsbestosAPI.Entities
{
    public class Element
    {
		int Id { get; set; }
		int RoomId { get; set; }
		string Description { get; set; }
		int PropertyId { get; set; }
		int OrderId { get; set; }
		string Uprn { get; set; }
		bool IsInspected { get; set; }
		bool IsDoesContainAsbestos { get; set; }
		bool IsDidContainAsbestos { get; set; }
		bool IsAnyToDos { get; set; }
		string CreatedBy { get; set; }
		string ModifiedBy { get; set; }
		DateTime DateOfModification { get; set; }
		DateTime DateOfCreation { get; set; }
		bool IsActive { get; set; }
		bool IsLiableToAsbestos { get; set; }
    }
}
