using System;

namespace Api.Attributes
{
    public class FileMimeTypeAttribute : Attribute
    {
        public string MimeType { get; }
        
        public FileMimeTypeAttribute(string mimeType)
        {
            MimeType = mimeType;
        }
    }
}