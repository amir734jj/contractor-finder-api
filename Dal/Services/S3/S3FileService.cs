using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Dal.Interfaces;

namespace Dal.Services.S3
{
    public class S3FileService : IFileService
    {
        private readonly IS3Service _s3Service;

        public S3FileService(IS3Service s3Service)
        {
            _s3Service = s3Service;
        }
        
        public async Task<bool> Upload(Guid fileKey, string fileName, byte[] data)
        {
            var response = await _s3Service.Upload(fileKey, data);

            return response.Status == HttpStatusCode.OK;
        }

        public async Task<byte[]> Download(Guid keyName)
        {
            var response = await _s3Service.Download(keyName);

            return response.Status == HttpStatusCode.OK ? response.Data : new byte[] { };
        }

        public async Task<List<Guid>> List()
        {
            var response = await _s3Service.List();

            return response;
        }

        public async Task<bool> Delete(Guid keyName)
        {
            var response = await _s3Service.Delete(keyName);

            return response.Status == HttpStatusCode.OK;
        }
    }
}