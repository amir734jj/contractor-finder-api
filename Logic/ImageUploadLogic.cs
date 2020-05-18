using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dal.Interfaces;
using Logic.Interfaces;
using Models.Internal;

namespace Logic
{
    public class ImageUploadLogic : IImageUploadLogic
    {
        private readonly IFileService _fileService;

        public ImageUploadLogic(IFileService fileService)
        {
            _fileService = fileService;
        }
        
        public async Task<Guid> Upload(BasicFile file)
        {
            // Randomly assign a key!
            var key = Guid.NewGuid();

            await _fileService.Upload(key, file);

            return key;
        }

        public async Task<BasicFile> Download(Guid id)
        {
            return await _fileService.Download(id);
        }

        public Task<List<Guid>> List()
        {
            return _fileService.List();
        }

        public async Task<bool> Delete(Guid keyName)
        {
            return await _fileService.Delete(keyName);
        }
    }
}