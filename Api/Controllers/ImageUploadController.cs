using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Attributes;
using Api.Extensions;
using Api.Middlewares.FileUpload;
using Logic.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.Constants;

namespace Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class ImageUploadController : Controller
    {
        private readonly IImageUploadLogic _imageUploadLogic;

        public ImageUploadController(IImageUploadLogic imageUploadLogic)
        {
            _imageUploadLogic = imageUploadLogic;
        }

        [FileUpload]
        [HttpPost]
        [Route("FileUpload")]
        public async Task<IActionResult> ImageUpload([FileMimeType("image/*")] IFormFile file, [FromQuery] string description)
        {
            var response = await _imageUploadLogic.Upload(await file.ToStream(), new Dictionary<string, string>
            {
                [ImageMetadataKey.Description] = description
            });

            return Ok(response);
        }
    }
}