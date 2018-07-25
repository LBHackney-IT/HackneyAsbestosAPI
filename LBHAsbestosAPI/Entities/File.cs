using System;
namespace LBHAsbestosAPI.Entities
{
    public class File
    {
        public string ContentType { get; set; }
        public long? ByteSize { get; set; }
        public Byte[] DataStream { get; set; }
    }
}
