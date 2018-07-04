using System;
using System.Linq;

namespace LBHAsbestosAPI.Validators
{
    public static class InspectionIdValidator
    {
        public static bool Validate(string inspectionId)
        {
            var validIdLength = 8;

            if (inspectionId.Length != validIdLength)
            {
                return false;
            }
            if (inspectionId.Any(c => !char.IsDigit(c)))
            {
                return false;
            }
            return true;
        }
    }
}
