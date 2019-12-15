using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Api.Attributes;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Api.Middlewares.FileUpload
{
    /// <summary>
    ///     File upload swagger filter
    /// </summary>
    public class FileUploadOperation : IOperationFilter
    {
        /// <summary>
        ///     Override default
        /// </summary>
        /// <param name="operation"></param>
        /// <param name="context"></param>
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            // Safely cast general ActionDescriptor to ControllerActionDescriptor which is 
            // a wrapper over MethodInfo then check if MethodInfo has any custom attributes
            // of type FileUploadAttribute
            if (context.ApiDescription.ActionDescriptor is ControllerActionDescriptor method
                && method.MethodInfo.GetCustomAttributes<FileUploadAttribute>().Any())
            {
                var mimeTypeTable = context.MethodInfo.GetParameters()
                    .Select(x => (x.Name, FileMimeTypeAttribute: x.GetCustomAttribute<FileMimeTypeAttribute>()))
                    .Where(x => x.FileMimeTypeAttribute != null)
                    .ToDictionary(x => x.Name, x => x.FileMimeTypeAttribute.MimeType);

                foreach (var (key, value) in operation.RequestBody.Content.Where(x => x.Key == "multipart/form-data")
                    .SelectMany(x => x.Value.Encoding))
                {
                    value.ContentType = mimeTypeTable.GetValueOrDefault(key, "application/octet-stream");
                }
            }
        }
    }
}