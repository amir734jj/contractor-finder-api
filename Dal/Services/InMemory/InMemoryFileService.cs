using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dal.Interfaces;
using StructureMap;

namespace Dal.Services.InMemory
{
    [Singleton]
    public class InMemoryFileService : IFileService
    {
        private readonly Dictionary<Guid, byte[]> _table = new Dictionary<Guid, byte[]>();
        
        public async Task<bool> Upload(Guid fileKey, string fileName, byte[] data)
        {
            _table[fileKey] = data;

            return true;
        }

        public async Task<byte[]> Download(Guid keyName)
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