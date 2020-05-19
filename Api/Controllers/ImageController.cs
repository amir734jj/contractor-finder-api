using System;
using System.Threading.Tasks;
using Logic.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Internal;
using Models.ViewModels;
using Swashbuckle.AspNetCore.JsonMultipartFormDataSupport;

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
        
        [HttpPost]
        [Route("upload")]
        [Consumes("multipart/form-data")] 
        public async Task<IActionResult> ImageUpload([FromForm] MultipartFormData<FileUploadViewModel> fileUploadViewModel)
        {
            if (fileUploadViewModel == null)
            {
                return BadRequest("Failed to upload file");
            }

            var response = await _imageUploadLogic.Upload(new BasicFile(fileUploadViewModel.File));

            return Ok(response);
        }
        
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> DownloadImage([FromRoute] Guid id)
        {
            var result = await _imageUploadLogic.Download(id);

            return File(result.Data, result.ContentType, result.Name);
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