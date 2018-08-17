using System;
using System.Collections.Generic;

namespace LBHAsbestosAPI.Entities
{
    public class Response<T>
    {
		public bool Success { get; set; }
        public string ErrorMesage { get; set; }
        public T Data { get; set; }
    }
}
