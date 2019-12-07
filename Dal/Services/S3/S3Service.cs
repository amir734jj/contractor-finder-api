using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Dal.Interfaces;
using Microsoft.Extensions.Logging;
using Models.ViewModels.Services.S3;

namespace Dal.Services.S3
{
    public class S3Service : IS3Service
    {
        private readonly IAmazonS3 _client;
        private readonly string _prefix;
        private readonly ILogger<S3Service> _logger;

        /// <summary>
        /// Constructor that takes a S3Client and a prefix for all paths
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="client"></param>
        /// <param name="prefix"></param>
        public S3Service(ILogger<S3Service> logger, IAmazonS3 client, string prefix)
        {
            _logger = logger;
            _client = client;
            _prefix = prefix;
        }

        /// <summary>
        ///     Upload a file to an S3, here four files are uploaded in four different ways
        /// </summary>
        /// <param name="bucketName">Bucket where the file is stored</param>
        /// <param name="fileKey"></param>
        /// <param name="data"></param>
        /// <param name="metadata"></param>
        /// <returns></returns>
        public async Task<SimpleS3Response> Upload(string bucketName, string fileKey, Stream data, IReadOnlyDictionary<string, string> metadata)
        {
            try
            {
                if (await _client.DoesS3BucketExistAsync(bucketName))
                {
                    var fileTransferUtility = new TransferUtility(_client);

                    var fileTransferUtilityRequest = new TransferUtilityUploadRequest
                    {
                        Key = $"{_prefix}/{fileKey}",
                        InputStream = data,
                        BucketName = bucketName,
                        CannedACL = S3CannedACL.NoACL
                    };

                    foreach (var (key, value) in metadata)
                    {
                        fileTransferUtilityRequest.Metadata.Add(key, value);
                    }

                    await fileTransferUtility.UploadAsync(fileTransferUtilityRequest);

                    return new SimpleS3Response(HttpStatusCode.OK, "Successfully uploaded to S3");
                }

                // Bucket not found
                throw new Exception($"Bucket: {bucketName} does not exist");
            }
            // Catch specific amazon errors
            catch (AmazonS3Exception e)
            {
                _logger.LogError(e.AmazonId2, e);
                
                return new SimpleS3Response(e.StatusCode, e.Message);
            }
            // Catch other errors
            catch (Exception e)
            {
                _logger.LogError(e.Message, e);
                
                return new SimpleS3Response(HttpStatusCode.BadRequest, e.Message);
            }
        }

        /// <summary>
        ///     Get a file from S3
        /// </summary>
        /// <param name="bucketName">Bucket where the file is stored</param>
        /// <param name="keyName">Key name of the bucket (File Name)</param>
        /// <returns></returns>
        public async Task<UriS3Response> GetUri(string bucketName, string keyName)
        {
            try
            {
                // Build the request with the bucket name and the keyName (name of the file)
                var preSignedUrlRequest = new GetPreSignedUrlRequest
                {
                    BucketName = bucketName,
                    Key = $"{_prefix}/{keyName}",
                    Expires = DateTime.Now.AddHours(1)
                };

                var urlString = _client.GetPreSignedURL(preSignedUrlRequest);

                var file = await Download(bucketName, keyName);

                return new UriS3Response(HttpStatusCode.OK, "Successfully generated Uri", new Uri(urlString),  file.MetaData, file.ContentType, file.Name);
            }
            // Catch specific amazon errors
            catch (AmazonS3Exception e)
            {
                _logger.LogError(e.AmazonId2, e);
                
                return new UriS3Response(e.StatusCode, e.Message);
            }
            // Catch other errors
            catch (Exception e)
            {
                _logger.LogError(e.Message, e);

                return new UriS3Response(HttpStatusCode.BadRequest, e.Message);
            }
        }
        
        public async Task<DownloadS3Response> Download(string bucketName, string keyName)
        {
            try
            {
                // Build the request with the bucket name and the keyName (name of the file)
                var request = new GetObjectRequest
                {
                    BucketName = bucketName,
                    Key = $"{_prefix}/{keyName}"
                };

                using var response = await _client.GetObjectAsync(request);
                await using var responseStream = response.ResponseStream;
                await using var memoryStream = new MemoryStream();
                var title = response.Metadata["x-amz-meta-title"];
                var contentType = response.Headers["Content-Type"];
                var metadata = response.Metadata.Keys.ToDictionary(x => x, x => response.Metadata[x]);

                // Copy stream to another stream
                responseStream.CopyTo(memoryStream);

                return new DownloadS3Response(HttpStatusCode.OK, "Successfully downloaded S3 object", memoryStream.ToArray(), metadata, contentType,
                    title);
            }
            // Catch specific amazon errors
            catch (AmazonS3Exception e)
            {
                _logger.LogError(e.AmazonId2, e);
                
                return new DownloadS3Response(e.StatusCode, e.Message);
            }
            // Catch other errors
            catch (Exception e)
            {
                _logger.LogError(e.Message, e);
                
                return new DownloadS3Response(HttpStatusCode.BadRequest, e.Message);
            }
        }
    }
}