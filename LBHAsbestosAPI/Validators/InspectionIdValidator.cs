using System;
using LBHAsbestosAPI.Entities;
using LBHAsbestosAPI.Interfaces;
using LBHAsbestosAPI.Models;

namespace LBHAsbestosAPI.Validators
{
    public class InspectionIdValidator
    {
        public InspectionValidationResult Validate(string requestId)
        {
            // TODO Inspection Id validation logic here
            var validateId = new InspectionValidationResult(requestId);
            validateId.Valid = true;
            validateId.ErrorMessages.Add("No error message yet");
            return validateId;
        }
    }

    public class InspectionValidationResult : ValidationResult
    {
        public InspectionValidationResult(string requestId) : base()
        {
            InspectionId = requestId;
        }
        public string InspectionId { get; set; }
    }
}
