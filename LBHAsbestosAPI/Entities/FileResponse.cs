using System;
namespace LBHAsbestosAPI.Entities
{
    public class FileResponse
    {
        public string ContentType { get; set; }
        public long? ByteSize { get; set; }
        public Byte[] DataStream { get; set; }
    }
}
