using System;
namespace LBHAsbestosAPI.Entities
{
    public class Document
    {
		public int Id { get; set; }
        public string Reference { get; set; }
        public string Description { get; set; }
        public int? JobId { get; set; } 
        public int PropertyId { get; set; } 
        public string Uprn { get; set; } 
        public string CreatedBy { get; set; } 
        public string ModifiedBy { get; set; } 
        public DateTime? DateOfModification { get; set; }
        public DateTime DateOfCreation { get; set; } 
        public bool IsApproved { get; set; }
    }
}
