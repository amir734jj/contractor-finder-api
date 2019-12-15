using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Logic.Interfaces
{
    public interface IImageUploadLogic
    {
        Task<Guid> Upload(Stream stream, IDictionary<string, string> metadata);
    }
}