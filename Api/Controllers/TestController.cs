using Api.Attributes;
using Api.Middlewares.FileUpload;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    public class TestController : Controller
    {
        [FileUpload]
        [HttpPost]
        [Route("FileUpload")]
        public IActionResult TestFileUpload([FileMimeType("image/*")] IFormFile file1, [FileMimeType("image/*")]IFormFile file2)
        {
            return Ok(file1.ContentType);
        }
    }
}