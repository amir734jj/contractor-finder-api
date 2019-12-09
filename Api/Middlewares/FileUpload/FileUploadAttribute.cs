using System;

namespace Api.Middlewares.FileUpload
{
    /// <summary>
    ///     File upload attribute, will be used by FileUploadOperation
    /// </summary>
    public class FileUploadAttribute : Attribute
    {
        /// <summary>
        ///     Constructor that takes MIME-Types
        /// </summary>
        /// <param name="accepts"></param>
        public FileUploadAttribute(params string[] accepts)
        {
            Accepts = accepts;
        }

        /// <summary>
        ///     Accepts MIME-Type
        /// </summary>
        public string[] Accepts { get; }
    }
}