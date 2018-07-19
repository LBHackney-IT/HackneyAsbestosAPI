using System;
using System.Collections.Generic;

namespace LBHAsbestosAPI.Entities
{
    public class RoomResponse
    {
	public bool Success { get; set; }
        public string ErrorMesage { get; set; }
        public Room Data { get; set; }
    }
}
