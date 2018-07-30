using System;
namespace LBHAsbestosAPI.Entities
{
    public class FileResponse
    {
        public string ContentType { get; set; }
        public long? Size { get; set; }
        public Byte[] Data { get; set; }
    }
}
