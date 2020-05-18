using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dal.Configs;
using Dal.Interfaces;
using FluentFTP;
using Models.Internal;

namespace Dal.Services.Ftp
{
    public class FtpFileService : IFileService
    {
        private readonly FtpServiceConfig _ftpSinkServiceConfig;
        
        private readonly FtpClient _client;

        public FtpFileService(FtpServiceConfig ftpSinkServiceConfig)
        {
            _ftpSinkServiceConfig = ftpSinkServiceConfig;

            _client = new FtpClient(ftpSinkServiceConfig.Host, ftpSinkServiceConfig.Port, ftpSinkServiceConfig.Username, ftpSinkServiceConfig.Password);
        }
        
        public async Task<bool> Upload(Guid fileKey, string fileName, byte[] data)
        {
            await Upload(fileKey.ToString(), data);

            return true;
        }

        public async Task<BasicFile> Download(Guid keyName)
        {
            var response = await _client.DownloadAsync(ResolveFile(keyName.ToString()), CancellationToken.None);

            return response;
        }

        public async Task<List<Guid>> List()
        {
            var response = await _client.GetListingAsync(ResolveDir());

            return response.Select(x => Guid.Parse(x.Name)).ToList();
        }

        public async Task<bool> Delete(Guid keyName)
        {
            await _client.DeleteFileAsync(ResolveDir());

            return true;
        }
        
        private async Task Upload(string filename, byte[] data)
        {
            await _client.ConnectAsync();

            await _client.CreateDirectoryAsync(ResolveDir());
            
            await _client.UploadAsync(data, Path.Join(ResolveDir(), filename));

            await _client.DisconnectAsync();
        }

        private string ResolveFile(string filename)
        {
            return Path.Join(ResolveDir(), filename);
        }
        
        private string ResolveDir()
        {
            return Path.Join(_ftpSinkServiceConfig.Path, "file-service");
        }
    }
}