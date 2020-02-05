using System;
using System.Collections.Generic;
using System.Net;

namespace Models.ViewModels.Services.S3
{
    public class DownloadS3Response : SimpleS3Response
    {
        public byte[] Data { get; }
        
        public IReadOnlyDictionary<string, string> MetaData { get; }

        public string ContentType { get; } = "application/octet-stream";

        public string Name { get; } = Guid.NewGuid().ToString();

        public DownloadS3Response(HttpStatusCode statusCode, string message) : base(statusCode, message)
        {
        }

        public DownloadS3Response(HttpStatusCode statusCode, string message, byte[] data, IReadOnlyDictionary<string, string> metaData) : base(statusCode, message)
        {
            Data = data;
            MetaData = metaData;
        }

        public DownloadS3Response(HttpStatusCode status, string message, byte[] data, IReadOnlyDictionary<string, string> metaData, string contentType, string name) : this(status, message, data, metaData)
        {
            ContentType = contentType;
            Name = name;
        }
    }
}