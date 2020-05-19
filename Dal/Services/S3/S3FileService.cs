using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Dal.Interfaces;
using Models.Internal;

namespace Dal.Services.S3
{
    public class S3FileService : IFileService
    {
        private readonly IS3Service _s3Service;

        private const string Name = nameof(Name);
        
        private const string ContentType = nameof(ContentType);

        public S3FileService(IS3Service s3Service)
        {
            _s3Service = s3Service;
        }
        
        public async Task<bool> Upload(Guid fileKey, BasicFile file)
        {
            var response = await _s3Service.Upload(fileKey, file.Data, new Dictionary<string, string>
            {
                [Name] = file.Name,
                [ContentType] = file.ContentType
            });

            return response.Status == HttpStatusCode.OK;
        }

        public async Task<BasicFile> Download(Guid keyName)
        {
            var response = await _s3Service.Download(keyName);

            if (response.Status == HttpStatusCode.OK)
            {
                return new BasicFile(response.Data, response.MetaData[Name], response.MetaData[ContentType]);
            }

            return null;
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