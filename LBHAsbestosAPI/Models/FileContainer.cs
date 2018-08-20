using System;
namespace LBHAsbestosAPI.Models
{
    public class FileContainer
    {
        public string ContentType { get; set; }
        public long? Size { get; set; }
        public Byte[] Data { get; set; }
    }
}
