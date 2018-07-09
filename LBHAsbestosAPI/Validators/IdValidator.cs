using System;
using System.Linq;
using LBHAsbestosAPI.Interfaces;

namespace LBHAsbestosAPI.Validators
{
    public static class IdValidator
    {
       
        public static bool ValidatePropertyId(string propertyId)
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

        public static bool ValidateRoomId(string roomId)
        {
            var validIdLength = 6;

            if (roomId.Length != validIdLength)
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
