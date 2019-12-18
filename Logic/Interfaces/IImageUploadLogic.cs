using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Models.ViewModels.Services.S3;

namespace Logic.Interfaces
{
    public interface IImageUploadLogic
    {
        Task<Guid> Upload(byte[] stream, IDictionary<string, string> metadata);

        Task<DownloadS3Response> Download(Guid id);
        
        Task<string> Url(Guid id);
        
        Task<List<Guid>> List();
    }
}