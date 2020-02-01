using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Models.ViewModels.Services.S3;

namespace Dal.Interfaces
{
    public interface IS3Service
    {
        Task<SimpleS3Response> Upload(Guid fileKey,
            byte[] data,
            IDictionary<string, string> metadata);

        Task<UriS3Response> GetUri(Guid keyName);

        Task<DownloadS3Response> Download(Guid keyName);

        Task<List<Guid>> List();

        Task<S3DeleteObjectResponse> Delete(Guid keyName);
    }
}