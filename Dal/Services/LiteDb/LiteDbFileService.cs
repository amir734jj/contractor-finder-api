using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dal.Interfaces;
using LiteDB;
using Models.Internal;
using StructureMap;

namespace Dal.Services.LiteDb
{
    class LiteDbFileRep
    {
        [BsonId]
        public Guid Key { get; set; }
        
        public BasicFile File { get; set; }
    }
    
    [Singleton]
    public class LiteDbFileService : IFileService
    {
        private readonly ILiteCollection<LiteDbFileRep> _collection;

        public LiteDbFileService(ILiteDatabase database)
        {
            _collection = database.GetCollection<LiteDbFileRep>();
        }
        
        public async Task<bool> Upload(Guid fileKey, BasicFile file)
        {
            _collection.Insert(new LiteDbFileRep {Key = fileKey, File = file});

            return true;
        }

        public async Task<BasicFile> Download(Guid keyName)
        {
            var result = _collection.Find(x => x.Key == keyName).FirstOrDefault();

            return result?.File;
        }

        public async Task<List<Guid>> List()
        {
            return _collection.FindAll().Select(x => x.Key).ToList();
        }

        public async Task<bool> Delete(Guid keyName)
        {
            var result = _collection.Find(x => x.Key == keyName).FirstOrDefault();

            if (result != null)
            {
                _collection.Delete(keyName);

                return true;
            }

            return false;
        }
    }
}