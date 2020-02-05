using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Dal.Interfaces;
using Models.ViewModels.Services.S3;

namespace Dal.Services.S3
{
    /// <summary>
    ///     In-memory implementation of S3 for local environment
    /// </summary>
    public class InMemoryS3 : IS3Service
    {
        private readonly Dictionary<Guid, (byte[] data, IReadOnlyDictionary<string, string> metadata)> _table;

        public InMemoryS3()
        {
            _table = new Dictionary<Guid, (byte[] data, IReadOnlyDictionary<string, string> metadata)>();
        }

        public async Task<SimpleS3Response> Upload(Guid fileKey, byte[] data, IDictionary<string, string> metadata)
        {
            _table[fileKey] = (data, metadata.ToDictionary(x => x.Key, x => x.Value));
            
            return new SimpleS3Response(HttpStatusCode.OK, "Successfully saved to dictionay");
        }

        public Task<UriS3Response> GetUri(Guid keyName)
        {
            throw new NotImplementedException();
        }

        public async Task<DownloadS3Response> Download(Guid keyName)
        {
            if (_table.ContainsKey(keyName))
            {
                var (data, metadata) = _table[keyName];
                
                return new DownloadS3Response(HttpStatusCode.OK, "Successfully fetched from dictionary", data, metadata);
            }

            return new DownloadS3Response(HttpStatusCode.BadRequest, "Failed to fetch from dictionary");
        }

        public async Task<List<Guid>> List()
        {
            return _table.Keys.ToList();
        }

        public async Task<S3DeleteObjectResponse> Delete(Guid keyName)
        {
            if (_table.ContainsKey(keyName))
            {
                _table.Remove(keyName);

                return new S3DeleteObjectResponse(HttpStatusCode.OK, "Successfully deleted from dictionary");
            }
            
            return new S3DeleteObjectResponse(HttpStatusCode.BadRequest, "Failed to delete from dictionary");
        }
    }
}