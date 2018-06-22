using System;
using LBHAsbestosAPI.Entities;
using LBHAsbestosAPI.Interfaces;

namespace LBHAsbestosAPI.Validators
{
    public class InspectionValidator : IRequestValidator
    {
        public InspectionResponse Validate(string requestID)
        {
            var validateResult = new InspectionResponse();
            validateResult.Success = true;
            validateResult.ErrorMesage = "No error message yet";
            return validateResult;
        }
    }
}
