using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Dal.Interfaces;
using Logic.Interfaces;

namespace Logic
{
    public class ImageUploadLogic : IImageUploadLogic
    {
        private readonly IS3Service _s3Service;

        public ImageUploadLogic(IS3Service s3Service)
        {
            _s3Service = s3Service;
        }
        
        public async Task<Guid> Upload(Stream stream, IDictionary<string, string> metadata)
        {
            var key = Guid.NewGuid();

            await _s3Service.Upload(key, stream, metadata);

            return key;
        }
    }
}