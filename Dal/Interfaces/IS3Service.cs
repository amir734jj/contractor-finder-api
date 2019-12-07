using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Models.ViewModels.Services.S3;

namespace Dal.Interfaces
{
    public interface IS3Service
    {
        Task<SimpleS3Response> Upload(string bucketName,
            string fileKey,
            Stream data,
            IReadOnlyDictionary<string, string> metadata);

        Task<UriS3Response> GetUri(string bucketName, string keyName);

        Task<DownloadS3Response> Download(string bucketName, string keyName);
    }
}