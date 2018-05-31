using System;
namespace LBHAsbestosAPI.Entities
{
    public class Floor
    {
		public virtual int Id { get; set; }
		public virtual int Description { get; set; }
		public virtual int PropertyId { get; set; }
		public virtual int OrderId { get; set; }
		public virtual int Uprn { get; set; }
		public virtual bool IsInspected { get; set; }
		public virtual bool IsDoesContainAsbestos { get; set; }
		public virtual bool IsDidContainAsbestos { get; set; }
		public virtual bool IsAnyToDos { get; set; }
		public virtual string CreatedBy { get; set; }
		public virtual string ModifiedBy { get; set; }
		public virtual DateTime DateOfModification { get; set; }
		public virtual DateTime DateOfCreation { get; set; }
		public virtual bool IsActive { get; set; }
		public virtual bool IsLiableToAsbestos { get; set; }
    }
}
