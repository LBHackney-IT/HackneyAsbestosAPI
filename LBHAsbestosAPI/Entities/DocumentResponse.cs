using System;
using System.Collections.Generic;

namespace LBHAsbestosAPI.Entities
{
    public class DocumentResponse
    {
		public bool Success { get; set; }
        public string ErrorMesage { get; set; }
        public List<Document> Data { get; set; }
    }
}
