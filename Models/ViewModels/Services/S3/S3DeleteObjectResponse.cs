using System.Net;

namespace Models.ViewModels.Services.S3
{
    public class S3DeleteObjectResponse : SimpleS3Response
    {
        public S3DeleteObjectResponse(HttpStatusCode status, string message) : base(status, message)
        {

        }
    }
}