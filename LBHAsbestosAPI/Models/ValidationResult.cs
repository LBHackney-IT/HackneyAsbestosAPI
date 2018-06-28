using System;
using System.Collections.Generic;

namespace LBHAsbestosAPI.Models
{
    public class ValidationResult
    {
        public ValidationResult()
        {
            ErrorMessages = new List<string>();
            Valid = true;
        }
        public bool Valid { get; set; }
        public List<string> ErrorMessages { get; set; }
    }
}
