using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Models.Internal;

namespace Logic.Interfaces
{
    public interface IImageUploadLogic
    {
        Task<Guid> Upload(BasicFile file);

        Task<BasicFile> Download(Guid id);

        Task<List<Guid>> List();

        Task<bool> Delete(Guid id);
    }
}