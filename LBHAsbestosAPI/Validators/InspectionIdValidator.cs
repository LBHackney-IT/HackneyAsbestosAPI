using System;
using System.Linq;

namespace LBHAsbestosAPI.Validators
{
    public static class InspectionIdValidator
    {
        public static bool Validate(string propertyId)
        {
            var validIdLength = 8;

            if (propertyId.Length != validIdLength)
            {
                return false;
            }
            if (propertyId.Any(c => !char.IsDigit(c)))
            {
                return false;
            }
            return true;
        }
    }
}
