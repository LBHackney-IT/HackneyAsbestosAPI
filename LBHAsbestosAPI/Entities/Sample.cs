using System;
namespace LBHAsbestosAPI.Entities
{
    public class Sample
    {
        public int Id { get; set; }
        public int InspectionId { get; set; }
        public string SampleNumber { get; set; }
        public DateTime AnalysisDate { get; set; }
        public string SampleReference { get; set; }
        public string AnalysedBy { get; set; }
        public int PropertyId { get; set; }
        public string Uprn { get; set; }
        public string RefferedSample { get; set; }
        public string CreatedBy { get; set; } 
        public string ModifiedBy { get; set; } 
        public DateTime? DateOfModification { get; set; }
        public DateTime DateOfCreation { get; set; }
    }
}
