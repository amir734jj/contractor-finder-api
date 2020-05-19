using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dal.Interfaces;
using Models.Internal;
using StructureMap;

namespace Dal.Services.InMemory
{
    [Singleton]
    public class InMemoryFileService : IFileService
    {
        private readonly Dictionary<Guid, BasicFile> _table = new Dictionary<Guid, BasicFile>();
        
        public async Task<bool> Upload(Guid fileKey, BasicFile file)
        {
            _table[fileKey] = file;

            return true;
        }

        public async Task<BasicFile> Download(Guid keyName)
        {
            return _table.GetValueOrDefault(keyName, null);
        }

        public async Task<List<Guid>> List()
        {
            return _table.Keys.ToList();
        }

        public async Task<bool> Delete(Guid keyName)
        {
            if (_table.ContainsKey(keyName))
            {
                _table.Remove(keyName);

                return true;
            }

            return false;
        }
    }
}