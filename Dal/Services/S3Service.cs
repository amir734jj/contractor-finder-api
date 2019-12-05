using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;

namespace Dal.Services
{
    public class S3Service
    {
        private readonly IAmazonS3 _client;

        public S3Service(IAmazonS3 client)
        {
            _client = client;
        }

        /// <summary>
        ///     Create a bucket, sending the Bucket Name
        /// </summary>
        /// <param name="bucketName">Bucket Name (string)</param>
        /// <returns></returns>
        public async Task<object> CreateBucket(string bucketName)
        {
            try
            {
                // Check if bucket exists, then create it
                if (!await _client.DoesS3BucketExistAsync(bucketName))
                {
                    var putBucketRequest = new PutBucketRequest
                    {
                        BucketName = bucketName,
                        UseClientRegion = true
                    };

                    // ReSharper disable once RedundantArgumentDefaultValue
                    var response = await _client.PutBucketAsync(putBucketRequest);

                    return new
                    {
                        Message = response?.ResponseMetadata?.RequestId,
                        Status = response?.HttpStatusCode
                    };
                }

                throw new Exception($"Bucket: {bucketName} already exist");
            }

            // Catch specific amazon errors
            catch (AmazonS3Exception e)
            {
                return new
                {
                    e.Message,
                    Status = e.StatusCode
                };
            }

            // Catch other errors
            catch (Exception e)
            {
                return new
                {
                    Message = e.Message,
                    Status = HttpStatusCode.InternalServerError
                };
            }
        }

        /// <summary>
        ///     Deleting a bucket, sending the Bucket Name
        /// </summary>
        /// <param name="bucketName">Bucket where the file is stored</param>
        /// <returns></returns>
        public async Task<object> DropBucket(string bucketName)
        {
            try
            {
                if (await _client.DoesS3BucketExistAsync(bucketName))
                {
                    // Delete the bucket and return the response
                    var response = await _client.DeleteBucketAsync(bucketName);

                    return new
                    {
                        Message = response?.ResponseMetadata?.RequestId,
                        Status = response.HttpStatusCode
                    };
                }

                throw new Exception($"Bucket: {bucketName} does not exist");
            }

            // Catch specific amazon errors
            catch (AmazonS3Exception e)
            {
                return new
                {
                    Message = e.Message,
                    Status = e.StatusCode
                };
            }

            // Catch other errors
            catch (Exception e)
            {
                return new
                {
                    Message = e.Message,
                    Status = HttpStatusCode.InternalServerError
                };
            }
        }

        /// <summary>
        ///     Upload a file to an S3, here four files are uploaded in four different ways
        /// </summary>
        /// <param name="bucketName">Bucket where the file is stored</param>
        /// <param name="fileKey"></param>
        /// <param name="data"></param>
        /// <param name="metadata"></param>
        /// <returns></returns>
        public async Task<object> Upload(string bucketName, string fileKey, byte[] data,
            IReadOnlyDictionary<string, string> metadata)
        {
            try
            {
                if (await _client.DoesS3BucketExistAsync(bucketName))
                {
                    var fileTransferUtility = new TransferUtility(_client);

                    var fileTransferUtilityRequest = new TransferUtilityUploadRequest
                    {
                        Key = fileKey,
                        InputStream = new MemoryStream(data),
                        BucketName = bucketName,
                        StorageClass = S3StorageClass.Standard,
                        PartSize = 6291456,
                        CannedACL = S3CannedACL.NoACL
                    };

                    foreach (var (key, value) in metadata)
                    {
                        fileTransferUtilityRequest.Metadata.Add(key, value);
                    }

                    await fileTransferUtility.UploadAsync(fileTransferUtilityRequest);
                }

                throw new Exception($"Bucket: {bucketName} does not exist");
            }

            // Catch specific amazon errors
            catch (AmazonS3Exception e)
            {
                return new
                {
                    Message = e.Message,
                    Status = e.StatusCode
                };
            }

            // Catch other errors
            catch (Exception e)
            {
                return new
                {
                    Message = e.Message,
                    Status = HttpStatusCode.InternalServerError
                };
            }
        }

        public async Task<object> List(string bucketName)
        {
            try
            {
                // Build the request with the bucket name and the keyName (name of the file)
                var request = new ListObjectsV2Request
                {
                    BucketName = bucketName,
                    MaxKeys = 10
                };

                ListObjectsV2Response response;
                var rslt = new List<object>();

                do
                {
                    response = await _client.ListObjectsV2Async(request);

                    // Process the response.
                    foreach (var entry in response.S3Objects)
                    {
                        rslt.Add(new
                        {
                            Key = entry.Key,
                            Size = entry.Size
                        });
                    }

                    request.ContinuationToken = response.NextContinuationToken;
                } while (response.IsTruncated);

                return new
                {
                    ListItem = rslt,
                    Message = "Successfully listed objects in the bucket",
                    Status = HttpStatusCode.OK
                };
            }

            // Catch specific amazon errors
            catch (AmazonS3Exception e)
            {
                return new
                {
                    Message = e.Message,
                    Status = e.StatusCode,
                    ListItem = new List<object>()
                };
            }

            // Catch other errors
            catch (Exception e)
            {
                return new
                {
                    Message = e.Message,
                    Status = HttpStatusCode.InternalServerError,
                    ListItem = new List<object>()
                };
            }
        }

        /// <summary>
        ///     Get a file from S3
        /// </summary>
        /// <param name="bucketName">Bucket where the file is stored</param>
        /// <param name="keyName">Key name of the bucket (File Name)</param>
        /// <returns></returns>
        public async Task<object> Download(string bucketName, string keyName)
        {
            try
            {
                // Build the request with the bucket name and the keyName (name of the file)
                var request = new GetObjectRequest
                {
                    BucketName = bucketName,
                    Key = keyName
                };

                using var response = await _client.GetObjectAsync(request);
                await using var responseStream = response.ResponseStream;
                await using var memstream = new MemoryStream();
                var title = response.Metadata["x-amz-meta-title"];
                var contentType = response.Headers["Content-Type"];
                var metadata = response.Metadata.Keys.ToDictionary(x => x, x => response.Metadata[x]);

                // Copy stream to another stream
                responseStream.CopyTo(memstream);

                return new
                {
                    Status = HttpStatusCode.OK,
                    Title = title,
                    ContentType = contentType,
                    Data = memstream.ToArray(),
                    Metadata = new ReadOnlyDictionary<string, string>(metadata)
                };
            }

            // Catch specific amazon errors
            catch (AmazonS3Exception e)
            {
                return new
                {
                    Message = e.Message,
                    Status = e.StatusCode,
                    ContentType = string.Empty,
                    Title = string.Empty,
                    Metadata = new ReadOnlyDictionary<string, string>(new Dictionary<string, string>())
                };
            }

            // Catch other errors
            catch (Exception e)
            {
                return new
                {
                    Message = e.Message,
                    Status = HttpStatusCode.InternalServerError,
                    Title = string.Empty,
                    Metadata = new ReadOnlyDictionary<string, string>(new Dictionary<string, string>())
                };
            }
        }

        /// <summary>
        ///     Delete a file from an S3
        /// </summary>
        /// <param name="bucketName">Bucket where file is stored</param>
        /// <param name="keyName">Key name of the file</param>
        /// <returns></returns>
        public async Task<object> Delete(string bucketName, string keyName)
        {
            if (string.IsNullOrEmpty(keyName))
            {
                throw new Exception("Key cannot be null or empty");
            }

            try
            {
                // Build the request with the bucket name and the keyName (name of the file)
                var request = new GetObjectRequest
                {
                    BucketName = bucketName,
                    Key = keyName
                };

                await _client.GetObjectAsync(request);

                await _client.DeleteObjectAsync(bucketName, keyName);

                return new
                {
                    Message = "The file was successfully deleted",
                    Status = HttpStatusCode.OK
                };
            }

            // Catch specific amazon errors
            catch (AmazonS3Exception e)
            {
                return new
                {
                    Message = e.Message,
                    Status = e.StatusCode
                };
            }

            // Catch other errors
            catch (Exception e)
            {
                return new
                {
                    Message = e.Message,
                    Status = HttpStatusCode.InternalServerError
                };
            }
        }
    }
}
