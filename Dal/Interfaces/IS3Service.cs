using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Models.ViewModels.Services.S3;

namespace Dal.Interfaces
{
    public interface IS3Service
    {
        Task<SimpleS3Response> Upload(
            Guid fileKey,
            Stream data,
            IReadOnlyDictionary<string, string> metadata);

        Task<UriS3Response> GetUri(Guid keyName);

        Task<DownloadS3Response> Download(Guid keyName);
    }
}