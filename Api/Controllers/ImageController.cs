using System;
using System.Collections.Immutable;
using System.Net;
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
    [ApiController]
    [Route("Api/[controller]")]
    public class ImageController : Controller
    {
        private readonly IImageUploadLogic _imageUploadLogic;

        public ImageController(IImageUploadLogic imageUploadLogic)
        {
            _imageUploadLogic = imageUploadLogic;
        }

        [FileUpload]
        [HttpPost]
        [Route("Upload")]
        public async Task<IActionResult> ImageUpload([FileMimeType("image/*")] IFormFile file,
            [FromQuery] string description)
        {
            var response = await _imageUploadLogic.Upload(
                await file.ToByteArray(),
                ImmutableDictionary<string, string>.Empty
                    .Add(ImageMetadataKey.Description, description)
            );

            return Ok(response);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> DownloadImage([FromRoute] Guid id)
        {
            var result = await _imageUploadLogic.Download(id);

            return result.Status == HttpStatusCode.OK
                ? (IActionResult) File(result.Data, result?.ContentType, result.Name)
                : BadRequest(result.Message);
        }
        
        [HttpGet]
        [Route("{id}/url")]
        public async Task<IActionResult> ImageUrl([FromRoute] Guid id)
        {
            var result = await _imageUploadLogic.Url(id);

            return Ok(result);
        }
        
        [HttpGet]
        [Route("")]
        public async Task<IActionResult> ListImages()
        {
            var result = await _imageUploadLogic.List();

            return Ok(result);
        }
    }
}