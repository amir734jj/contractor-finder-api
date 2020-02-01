using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Api.Extensions;
using Api.Middlewares.FileUpload;
using Logic.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.Constants;
using Models.ViewModels;

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

        //[FileUpload]
        [HttpPost]
        [Route("upload/base64")]
        public async Task<IActionResult> ImageUploadBase65([FromBody] Base64UploadViewModel file)
        {
            if (file?.Base64?.Length == null)
            {
                return BadRequest("Failed to upload file");
            }

            var response = await _imageUploadLogic.Upload(
                Convert.FromBase64String(file.Base64),
                new Dictionary<string, string>
                {
                    [ImageMetadataKey.Description] = file.Description,
                    [ImageMetadataKey.Name] = file.Name
                }
            );

            return Ok(response);
        }

        [FileUpload]
        [HttpPost]
        [Route("upload")]
        public async Task<IActionResult> ImageUpload([FromForm] IFormFile file, [FromQuery] string description)
        {
            if (file == null)
            {
                return BadRequest("Failed to upload file");
            }

            var response = await _imageUploadLogic.Upload(
                await file.ToByteArray(),
                new Dictionary<string, string> { [ImageMetadataKey.Description] = description }
            );

            return Ok(response);
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> DownloadImage([FromRoute] Guid id)
        {
            var result = await _imageUploadLogic.Download(id);

            if (result.Status == HttpStatusCode.OK)
            {
                return File(result.Data, result.ContentType, result.Name);
            }

            return BadRequest(result.Message);
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

        [HttpDelete]
        [Route("{id}/delete")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var result = await _imageUploadLogic.Delete(id);

            return Ok(result);
        }
    }
}