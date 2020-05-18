using Microsoft.AspNetCore.Http;

namespace Models.ViewModels
{
    public class FileUploadViewModel
    {
        public IFormFile File { get; set; }
    }
}