using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Dal.Interfaces;
using Logic.Interfaces;
using Models.ViewModels.Services.S3;

namespace Logic
{
    public class ImageUploadLogic : IImageUploadLogic
    {
        private readonly IS3Service _s3Service;

        public ImageUploadLogic(IS3Service s3Service)
        {
            _s3Service = s3Service;
        }
        
        public async Task<Guid> Upload(byte[] stream, IDictionary<string, string> metadata)
        {
            // Randomly assign a key!
            var key = Guid.NewGuid();

            await _s3Service.Upload(key, stream, metadata);

            return key;
        }

        public async Task<DownloadS3Response> Download(Guid id)
        {
            return await _s3Service.Download(id);
        }

        public async Task<string> Url(Guid id)
        {
            return (await _s3Service.GetUri(id))?.Uri?.AbsoluteUri;
        }

        public Task<List<Guid>> List()
        {
            return _s3Service.List();
        }

        public async Task<bool> Delete(Guid keyName)
        {
            return (await _s3Service.Delete(keyName))?.Status switch
            {
                HttpStatusCode.OK => true,
                _ => false
            };
        }
    }
}