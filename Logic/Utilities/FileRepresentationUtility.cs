using Models.Abstracts;
using Models.ViewModels.Services.S3;

namespace Logic.Utilities
{
    public static class FileRepresentationUtility
    {
        public static T ResolveFileRepresentation<T>(UriS3Response uriS3Response) where T : AbstractFileEntity, new()
        {
            var instance = new T
            {
                ContentType = uriS3Response.ContentType,
                Metadata = uriS3Response.MetaData,
                Name = uriS3Response.Name,
                Uri = uriS3Response.Uri
            };

            return instance;
        }
    }
}