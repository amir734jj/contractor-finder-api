using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Models.Internal;

namespace Dal.Interfaces
{
    public interface IFileService
    {
        Task<bool> Upload(Guid fileKey, BasicFile file);

        Task<BasicFile> Download(Guid keyName);

        Task<List<Guid>> List();

        Task<bool> Delete(Guid keyName);
    }
}