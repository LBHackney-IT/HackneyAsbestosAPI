using System;
using System.Linq;
using LBHAsbestosAPI.Interfaces;

namespace LBHAsbestosAPI.Validators
{
    public static class IdValidator
    {
       
        public static bool ValidatePropertyId(string propertyId)
        {
            // TODO current validation constrains are temporal and have yet to be reviewed 
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

        public static bool ValidateRoomId(string roomId)
        {
            var validIdMaxLength = 7;

            if (roomId.Length >= validIdMaxLength)
            {
                return false;
            }
            if (roomId.Any(c => !char.IsDigit(c)))
            {
                return false;
            }
            return true;
        }


    }
}
