using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Api.Extensions
{
    public static class FormFileExtension
    {
        public static async Task<Stream> ToStream(this IFormFile formFile)
        {
            await using var data = new MemoryStream();
            await formFile.CopyToAsync(data);
            return data;
        }
    }
}