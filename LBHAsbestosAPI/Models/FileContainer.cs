using System;
namespace LBHAsbestosAPI.Entities
{
    public class FileContainer
    {
        public string ContentType { get; set; }
        public long? Size { get; set; }
        public Byte[] Data { get; set; }
    }
}
