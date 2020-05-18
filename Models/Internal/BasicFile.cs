using System.IO;
using Microsoft.AspNetCore.Http;

namespace Models.Internal
{
    public class BasicFile
    {
        public BasicFile(IFormFile file)
        {
            Name = file.Name;
            ContentType = file.ContentType;
            Data = AsByteArray(file);
        }

        public byte[] Data { get; }
        
        public string Name { get; }
        
        public string ContentType { get; }

        private static byte[] AsByteArray(IFormFile file)
        {
            using var memoryStream = new MemoryStream();
            
            file.CopyTo(memoryStream);
            
            return memoryStream.ToArray();
        }
    }
}