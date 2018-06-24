using System;
namespace LBHAsbestosAPI.Entities
{
    public class Drawing
    {
		public int Id { get; set; }
        public string Reference { get; set; }
        public string Description { get; set; }
        public int JobId { get; set; } 
        public int PropertyId { get; set; } 
        public string Uprn { get; set; } 
        public string CreatedBy { get; set; } 
        public string ModifiedBy { get; set; } 
        public string DateOfModification { get; set; }
        public string DateOfCreation { get; set; } 
        public bool IsApproved { get; set; }
    }
}
