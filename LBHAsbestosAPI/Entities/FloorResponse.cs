using System;
using System.Collections.Generic;

namespace LBHAsbestosAPI.Entities
{
    public class FloorResponse
    {
		public bool Success { get; set; }
        public string ErrorMesage { get; set; }
        public List<Floor> Data { get; set; }
    }
}
