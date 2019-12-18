using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Api.Extensions
{
    public static class FormFileExtension
    {
        public static async Task<byte[]> ToByteArray(this IFormFile formFile)
        {
            await using var data = new MemoryStream();
            await formFile.CopyToAsync(data);

            // Seek begining
            data.Seek(0, SeekOrigin.Begin);

            return data.ToArray();
        }
    }
}